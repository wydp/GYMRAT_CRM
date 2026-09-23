using CRM.domain.Entities;
using CRM.infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CRM.api.Controllers
{
    [ApiController]
    [Route("staff")]
    [Authorize]
    public class StaffController : ControllerBase
    {
        private readonly MasterCrmDbContext _masterDb;

        public StaffController(MasterCrmDbContext masterDb)
        {
            _masterDb = masterDb;
        }

        // ---- request/response shapes ----

        public class CreateStaffRequest
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public string? Email { get; set; }
            public string RoleName { get; set; } = string.Empty;
            public int? BranchId { get; set; }
        }

        public class UpdateStaffRequest
        {
            public string Username { get; set; } = string.Empty;
            public string RoleName { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public string? Email { get; set; }
            public int? BranchId { get; set; }
            public bool IsActive { get; set; }
        }

        public class ResetPasswordRequest
        {
            public string NewPassword { get; set; } = string.Empty;
        }

        public class StaffResponse
        {
            public int UserId { get; set; }
            public string Username { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public string? Email { get; set; }
            public string RoleName { get; set; } = string.Empty;
            public int? CompanyId { get; set; }
            public int? BranchId { get; set; }
            public bool IsActive { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime? LastLoginAt { get; set; }
        }

        // ---- helper: current user from JWT ----

        private int? GetCurrentUserId()
        {
            var idStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(idStr, out var id) ? id : null;
        }

        private bool HasPermission(string code)
        {
            return User.Claims.Any(c => c.Type == "permission" && c.Value == code);
        }

        private static StaffResponse ToResponse(User u) => new()
        {
            UserId = u.UserId,
            Username = u.Username,
            FullName = u.FullName,
            Email = u.Email,
            RoleName = u.Role?.RoleName ?? "Unknown",
            CompanyId = u.CompanyId,
            BranchId = u.BranchId,
            IsActive = u.IsActive,
            CreatedAt = u.CreatedAt,
            LastLoginAt = u.LastLoginAt,
        };

        // ============================================================
        // GET /staff  — list staff in the current user's company
        // ============================================================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!HasPermission("staff.access"))
                return Forbid();

            var currentUserId = GetCurrentUserId();
            if (currentUserId is null) return Unauthorized();

            var me = await _masterDb.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == currentUserId.Value);
            if (me is null) return Unauthorized();

            var query = _masterDb.Users
                .Include(u => u.Role)
                .AsNoTracking()
                .AsQueryable();

            if (me.CompanyId.HasValue)
                query = query.Where(u => u.CompanyId == me.CompanyId.Value);

            if (me.Role != null && me.Role.RoleName == "Manager" && me.BranchId.HasValue)
                query = query.Where(u => u.BranchId == me.BranchId.Value);

            var staff = await query
                .Where(u => u.Role != null && u.Role.RoleName != "SuperAdmin")
                .OrderBy(u => u.UserId)
                .ToListAsync();

            return Ok(staff.Select(ToResponse).ToList());
        }

        // ============================================================
        // GET /staff/{id}
        // ============================================================
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!HasPermission("staff.access"))
                return Forbid();

            var user = await _masterDb.Users
                .Include(u => u.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user is null) return NotFound();
            return Ok(ToResponse(user));
        }

        // ============================================================
        // POST /staff  — create a new staff account
        // ============================================================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStaffRequest request)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId is null) return Unauthorized();

            var me = await _masterDb.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == currentUserId.Value);
            if (me is null) return Unauthorized();

            if (request.RoleName == "Manager")
            {
                if (!HasPermission("staff.create_manager"))
                    return StatusCode(403, new { message = "You do not have permission to create Manager accounts." });
            }
            else if (request.RoleName == "Cashier")
            {
                if (!HasPermission("staff.create_cashier"))
                    return StatusCode(403, new { message = "You do not have permission to create Cashier accounts." });
            }
            else
            {
                return BadRequest(new { message = "RoleName must be 'Manager' or 'Cashier'." });
            }

            if (string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password) ||
                string.IsNullOrWhiteSpace(request.FullName))
            {
                return BadRequest(new { message = "Username, password, and full name are required." });
            }

            if (await _masterDb.Users.AnyAsync(u => u.Username == request.Username))
                return Conflict(new { message = "This username is already taken." });

            var role = await _masterDb.Roles.FirstOrDefaultAsync(r => r.RoleName == request.RoleName);
            if (role is null)
                return BadRequest(new { message = $"Role '{request.RoleName}' does not exist." });

            if (!request.BranchId.HasValue)
                return BadRequest(new { message = "BranchId is required for Manager and Cashier accounts." });

            var newUser = new User
            {
                Username = request.Username.Trim(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FullName = request.FullName.Trim(),
                Email = request.Email?.Trim(),
                RoleId = role.RoleId,
                CompanyId = me.CompanyId,
                BranchId = request.BranchId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            };

            _masterDb.Users.Add(newUser);
            await _masterDb.SaveChangesAsync();

            newUser.Role = role;
            return CreatedAtAction(nameof(GetById), new { id = newUser.UserId }, ToResponse(newUser));
        }

        // ============================================================
        // PUT /staff/{id}  — update username, role, name, email, branch, active status
        // ============================================================
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStaffRequest request)
        {
            if (!HasPermission("staff.access"))
                return Forbid();

            var user = await _masterDb.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user is null) return NotFound();

            if (user.Role?.RoleName == "SuperAdmin")
                return StatusCode(403, new { message = "Cannot edit SuperAdmin accounts." });

            var currentUserId = GetCurrentUserId();
            if (currentUserId == id && !request.IsActive)
                return BadRequest(new { message = "You cannot deactivate your own account." });

            // ---- Username change (if provided and different) ----
            var newUsername = request.Username?.Trim();
            if (!string.IsNullOrEmpty(newUsername) && newUsername != user.Username)
            {
                if (await _masterDb.Users.AnyAsync(u => u.Username == newUsername && u.UserId != id))
                    return Conflict(new { message = "This username is already taken." });

                user.Username = newUsername;
            }

            // ---- Role change (if provided and different) ----
            var newRoleName = request.RoleName?.Trim();
            if (!string.IsNullOrEmpty(newRoleName) && newRoleName != user.Role?.RoleName)
            {
                if (newRoleName == "Manager" && !HasPermission("staff.create_manager"))
                    return StatusCode(403, new { message = "You do not have permission to assign the Manager role." });

                if (newRoleName == "Cashier" && !HasPermission("staff.create_cashier"))
                    return StatusCode(403, new { message = "You do not have permission to assign the Cashier role." });

                if (newRoleName == "SuperAdmin")
                    return StatusCode(403, new { message = "Cannot assign the SuperAdmin role." });

                var newRole = await _masterDb.Roles.FirstOrDefaultAsync(r => r.RoleName == newRoleName);
                if (newRole is null)
                    return BadRequest(new { message = $"Role '{newRoleName}' does not exist." });

                user.RoleId = newRole.RoleId;
            }

            // ---- Regular fields ----
            user.FullName = request.FullName.Trim();
            user.Email = request.Email?.Trim();
            user.BranchId = request.BranchId;
            user.IsActive = request.IsActive;

            await _masterDb.SaveChangesAsync();

            // Reload role navigation for the response
            await _masterDb.Entry(user).Reference(u => u.Role).LoadAsync();

            return Ok(ToResponse(user));
        }

        // ============================================================
        // DELETE /staff/{id}  — soft delete (deactivate)
        // ============================================================
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Deactivate(int id)
        {
            if (!HasPermission("staff.access"))
                return Forbid();

            var user = await _masterDb.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user is null) return NotFound();

            if (user.Role?.RoleName == "SuperAdmin")
                return StatusCode(403, new { message = "Cannot deactivate SuperAdmin accounts." });

            var currentUserId = GetCurrentUserId();
            if (currentUserId == id)
                return BadRequest(new { message = "You cannot deactivate your own account." });

            user.IsActive = false;
            await _masterDb.SaveChangesAsync();
            return Ok(ToResponse(user));
        }

        // ============================================================
        // POST /staff/{id}/reset-password
        // ============================================================
        [HttpPost("{id:int}/reset-password")]
        public async Task<IActionResult> ResetPassword(int id, [FromBody] ResetPasswordRequest request)
        {
            if (!HasPermission("staff.access"))
                return Forbid();

            if (string.IsNullOrWhiteSpace(request.NewPassword) || request.NewPassword.Length < 6)
                return BadRequest(new { message = "New password must be at least 6 characters." });

            var user = await _masterDb.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user is null) return NotFound();

            if (user.Role?.RoleName == "SuperAdmin")
                return StatusCode(403, new { message = "Cannot reset SuperAdmin passwords through this endpoint." });

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            await _masterDb.SaveChangesAsync();

            return Ok(new { message = $"Password reset for {user.Username}." });
        }
    }
}