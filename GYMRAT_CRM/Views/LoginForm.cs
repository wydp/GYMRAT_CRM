using CRM.winforms.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows.Forms;

namespace CRM.winforms
{
    public partial class LoginForm : Form
    {
        private readonly ApiClient _api = new ApiClient();

        public LoginForm()
        {
            InitializeComponent();
            WireEvents();
        }

        private void WireEvents()
        {
            btnLogin.Click += async (s, e) => await AttemptLoginAsync();

            txtPassword.KeyDown += async (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    await AttemptLoginAsync();
                }
            };
        }

        private async System.Threading.Tasks.Task AttemptLoginAsync()
        {
            lblError.Text = "";

            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                lblError.Text = "Please enter both username and password.";
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text = "Signing in...";

            try
            {
                var result = await _api.LoginAsync(username, password);

                if (result is null)
                {
                    lblError.Text = "Invalid username or password.";
                    txtPassword.Clear();
                    txtPassword.Focus();
                    return;
                }

                AuthContext.SetSession(
                    result.UserId,
                    result.Username,
                    result.FullName,
                    result.RoleName,
                    result.CompanyId,
                    result.BranchId,
                    result.Permissions,
                    result.Token,
                    result.ExpiresAt);

                // Start background sync manager (runs periodically) - non-blocking
                try
                {
                    if (result.CompanyId.HasValue)
                        CRM.winforms.Sync.SyncManager.Instance.Start(result.CompanyId.Value, _api, TimeSpan.FromSeconds(30));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to start sync manager: {ex.Message}");
                }

                // Allow the first sync to run in background; show main UI immediately so app is responsive
                var main = new Form1();
                main.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                lblError.Text = $"Login failed: {ex.Message}";
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Sign In";
            }
        }
    }
}