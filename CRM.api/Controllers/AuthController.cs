using CRM.domain.Entities;
using CRM.infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRM.api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly MasterCrmDbContext _masterDb;
        private readonly IConfiguration _config;

        public AuthController(MasterCrmDbContext masterDb, IConfiguration config)
        {
            _masterDb = masterDb;
            _config = config;
        }

        public class LoginRequest
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public class LoginResponse
        {
            public int UserId { get; set; }
            public string Username { get; set; } = string.Empty;
            public string FullName { get; set; } = string.Empty;
            public string RoleName { get; set; } = string.Empty;
            public int? CompanyId { get; set; }
            public int? BranchId { get; set; }
            public List<string> Permissions { get; set; } = new();
            public string Token { get; set; } = string.Empty;
            public DateTime ExpiresAt { get; set; }
        }

        // POST /auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest(new { message = "Username and password are required." });

            var user = await _masterDb.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            // Verify user exists, is active, and password matches
            if (user is null || !user.IsActive)
                return Unauthorized(new { message = "Invalid username or password." });

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Unauthorized(new { message = "Invalid username or password." });

            // Load permission codes for this user's role
            var permissions = await _masterDb.RolePermissions
                .Where(rp => rp.RoleId == user.RoleId)
                .Join(_masterDb.Permissions,
                      rp => rp.PermissionId,
                      p => p.PermissionId,
                      (rp, p) => p.Code)
                .ToListAsync();

            // Update LastLoginAt
            user.LastLoginAt = DateTime.UtcNow;
            await _masterDb.SaveChangesAsync();

            // Build JWT
            var jwtSection = _config.GetSection("Jwt");
            var keyStr = jwtSection["Key"] ?? throw new InvalidOperationException("JWT Key not configured.");
            var issuer = jwtSection["Issuer"] ?? "GymRat";
            var audience = jwtSection["Audience"] ?? "GymRatClient";
            var expiryHours = int.TryParse(jwtSection["ExpiryHours"], out var h) ? h : 8;
            var expiresAt = DateTime.UtcNow.AddHours(expiryHours);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.Role, user.Role?.RoleName ?? "Unknown"),
                new("companyId", user.CompanyId?.ToString() ?? ""),
                new("branchId", user.BranchId?.ToString() ?? ""),
            };

            // Add each permission as a claim so the API can authorize per-endpoint later
            foreach (var p in permissions)
                claims.Add(new Claim("permission", p));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyStr));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new LoginResponse
            {
                UserId = user.UserId,
                Username = user.Username,
                FullName = user.FullName,
                RoleName = user.Role?.RoleName ?? "Unknown",
                CompanyId = user.CompanyId,
                BranchId = user.BranchId,
                Permissions = permissions,
                Token = tokenString,
                ExpiresAt = expiresAt,
            });
        }

        // GET /auth/me — requires a valid JWT; returns the current user info
        [HttpGet("me")]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Me()
        {
            // The JWT middleware populated User.Claims from the token.
            var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var user = await _masterDb.Users
                .Include(u => u.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user is null) return NotFound();

            return Ok(new
            {
                user.UserId,
                user.Username,
                user.FullName,
                Role = user.Role?.RoleName,
                user.CompanyId,
                user.BranchId,
                Permissions = User.Claims
                    .Where(c => c.Type == "permission")
                    .Select(c => c.Value)
                    .ToList()
            });
        }
    }
}