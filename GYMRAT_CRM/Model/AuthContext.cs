using System;
using System.Collections.Generic;

namespace CRM.winforms.Model
{
    // Holds the currently logged-in user's session info. Static because every
    // UserControl and the ApiClient need to read from it, and creating a new
    // ApiClient per control would otherwise lose the token.
    //
    // Populated by LoginForm after a successful login. Cleared on logout.
    public static class AuthContext
    {
        public static int? UserId { get; private set; }
        public static string Username { get; private set; } = string.Empty;
        public static string FullName { get; private set; } = string.Empty;
        public static string RoleName { get; private set; } = string.Empty;
        public static int? CompanyId { get; private set; }
        public static int? BranchId { get; private set; }
        public static string Token { get; private set; } = string.Empty;
        public static DateTime? ExpiresAt { get; private set; }

        private static readonly HashSet<string> _permissions = new();

        public static bool IsLoggedIn => !string.IsNullOrEmpty(Token) && UserId.HasValue;

        public static IReadOnlyCollection<string> Permissions => _permissions;

        public static void SetSession(
            int userId,
            string username,
            string fullName,
            string roleName,
            int? companyId,
            int? branchId,
            IEnumerable<string> permissions,
            string token,
            DateTime expiresAt)
        {
            UserId = userId;
            Username = username;
            FullName = fullName;
            RoleName = roleName;
            CompanyId = companyId;
            BranchId = branchId;
            Token = token;
            ExpiresAt = expiresAt;

            _permissions.Clear();
            foreach (var p in permissions) _permissions.Add(p);
        }

        public static void Clear()
        {
            UserId = null;
            Username = string.Empty;
            FullName = string.Empty;
            RoleName = string.Empty;
            CompanyId = null;
            BranchId = null;
            Token = string.Empty;
            ExpiresAt = null;
            _permissions.Clear();
        }

        // --- Permission checks used by the sidebar and by controls ---

        public static bool HasPermission(string code)
        {
            return _permissions.Contains(code);
        }

        public static bool HasAnyPermission(params string[] codes)
        {
            foreach (var c in codes)
                if (_permissions.Contains(c)) return true;
            return false;
        }
    }
}