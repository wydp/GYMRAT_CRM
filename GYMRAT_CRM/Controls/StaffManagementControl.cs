using CRM.winforms.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CRM.winforms.Controls
{
    public partial class StaffManagementControl : UserControl
    {
        private readonly ApiClient _api = new ApiClient();
        private int? _selectedUserId = null;

        // Branch id -> branch name, populated once when the control first loads.
        private Dictionary<int, string> _branchNamesById = new();

        public StaffManagementControl()
        {
            InitializeComponent();
            StyleGrid();
            WireEvents();
            ConfigurePermissionAwareUI();
            _ = LoadInitialAsync();
        }

        private async System.Threading.Tasks.Task LoadInitialAsync()
        {
            await LoadBranchesAsync();
            await LoadStaffAsync();
        }

        // ------------------------------------------------------------------
        // Permission-aware UI setup
        // ------------------------------------------------------------------

        private void ConfigurePermissionAwareUI()
        {
            cmbRole.Items.Clear();

            if (AuthContext.HasPermission("staff.create_manager"))
                cmbRole.Items.Add("Manager");

            if (AuthContext.HasPermission("staff.create_cashier"))
                cmbRole.Items.Add("Cashier");

            if (cmbRole.Items.Count > 0)
                cmbRole.SelectedIndex = 0;

            if (!AuthContext.HasPermission("staff.create_manager") &&
                !AuthContext.HasPermission("staff.create_cashier"))
            {
                btnAdd.Visible = false;
                btnNew.Visible = false;
                lblHint.Text = "You do not have permission to create staff accounts.";
            }
        }

        private void StyleGrid()
        {
            dgvStaff.BorderStyle = BorderStyle.None;
            dgvStaff.RowHeadersVisible = false;
            dgvStaff.AllowUserToAddRows = false;
            dgvStaff.AllowUserToDeleteRows = false;
            dgvStaff.ReadOnly = true;
            dgvStaff.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStaff.MultiSelect = false;
            dgvStaff.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStaff.EnableHeadersVisualStyles = false;
            dgvStaff.GridColor = Color.FromArgb(229, 231, 235);
            dgvStaff.ColumnHeadersHeight = 40;
            dgvStaff.RowTemplate.Height = 36;

            dgvStaff.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237, 242, 255);
            dgvStaff.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvStaff.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            dgvStaff.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvStaff.DefaultCellStyle.SelectionBackColor = Color.FromArgb(72, 128, 255);
            dgvStaff.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvStaff.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvStaff.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);

            dgvStaff.Columns.Add("Username", "Username");
            dgvStaff.Columns.Add("FullName", "Full Name");
            dgvStaff.Columns.Add("Email", "Email");
            dgvStaff.Columns.Add("Role", "Role");
            dgvStaff.Columns.Add("BranchId", "Branch");
            dgvStaff.Columns.Add("Status", "Status");
            dgvStaff.Columns.Add("LastLogin", "Last Login");
        }

        private void WireEvents()
        {
            btnNew.Click += (s, e) => ClearForm();
            btnAdd.Click += async (s, e) => await AddStaffAsync();
            btnUpdate.Click += async (s, e) => await UpdateStaffAsync();
            btnDeactivate.Click += async (s, e) => await DeactivateStaffAsync();
            btnResetPassword.Click += async (s, e) => await ResetPasswordAsync();
            btnRefresh.Click += async (s, e) => await LoadStaffAsync();
            dgvStaff.SelectionChanged += DgvStaff_SelectionChanged;
        }

        // ------------------------------------------------------------------
        // Data loading
        // ------------------------------------------------------------------

        private async System.Threading.Tasks.Task LoadStaffAsync()
        {
            try
            {
                

                var staff = await _api.GetStaffAsync();
                dgvStaff.Rows.Clear();

                foreach (var s in staff.OrderBy(x => x.UserId))
                {
                    string branchDisplay = "—";
                    if (s.BranchId.HasValue)
                    {
                        if (_branchNamesById.TryGetValue(s.BranchId.Value, out var name))
                            branchDisplay = name;
                        else
                            branchDisplay = $"Branch #{s.BranchId.Value}";
                    }

                    var row = new DataGridViewRow();
                    row.CreateCells(dgvStaff,
                        s.Username,
                        s.FullName,
                        s.Email ?? string.Empty,
                        s.RoleName,
                        branchDisplay,
                        s.IsActive ? "Active" : "Inactive",
                        s.LastLoginAt?.ToString("yyyy-MM-dd HH:mm") ?? "Never");
                    row.Tag = s.UserId;

                    int idx = dgvStaff.Rows.Add(row);

                    dgvStaff.Rows[idx].Cells["Status"].Style.ForeColor =
                        s.IsActive ? Color.FromArgb(21, 128, 61) : Color.FromArgb(185, 28, 28);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load staff: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task LoadBranchesAsync()
        {
            try
            {
                var branches = await _api.GetBranchesAsync();
                _branchNamesById = branches.ToDictionary(b => b.BranchId, b => b.BranchName);

                cmbBranch.DataSource = branches;
                cmbBranch.DisplayMember = "BranchName";
                cmbBranch.ValueMember = "BranchId";
                cmbBranch.SelectedIndex = branches.Count > 0 ? 0 : -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load branches: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ------------------------------------------------------------------
        // Selection
        // ------------------------------------------------------------------

        private void DgvStaff_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvStaff.SelectedRows.Count == 0)
            {
                _selectedUserId = null;
                txtUsername.ReadOnly = false;
                txtUsername.BackColor = Color.White;
                return;
            }

            var row = dgvStaff.SelectedRows[0];
            if (row.Tag is not int userId) return;
            _selectedUserId = userId;

            txtUsername.Text = row.Cells["Username"].Value?.ToString();
            txtFullName.Text = row.Cells["FullName"].Value?.ToString();
            txtEmail.Text = row.Cells["Email"].Value?.ToString();

            // Lock username — cannot be changed after creation
            txtUsername.ReadOnly = false;
            txtUsername.BackColor = Color.FromArgb(240, 240, 240);
            txtPassword.Text = "";

            var roleName = row.Cells["Role"].Value?.ToString();
            if (roleName != null && cmbRole.Items.Contains(roleName))
                cmbRole.SelectedItem = roleName;
            

            if (int.TryParse(row.Cells["BranchId"].Value?.ToString(), out var branchId))
            {
                if (cmbBranch.DataSource is List<CRM.domain.Entities.Branch>)
                    cmbBranch.SelectedValue = branchId;
            }

            chkActive.Checked = row.Cells["Status"].Value?.ToString() == "Active";
        }

        // ------------------------------------------------------------------
        // Actions
        // ------------------------------------------------------------------

        private async System.Threading.Tasks.Task AddStaffAsync()
        {
            if (!ValidateAddForm()) return;

            var request = new ApiClient.CreateStaffRequest
            {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text,
                FullName = txtFullName.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                RoleName = cmbRole.SelectedItem?.ToString() ?? "",
                BranchId = cmbBranch.SelectedValue as int?,
            };

            try
            {
                await _api.CreateStaffAsync(request);
                MessageBox.Show($"Staff account created: {request.Username}", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                await LoadStaffAsync();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Cannot create staff",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not create staff: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task UpdateStaffAsync()
        {
            if (_selectedUserId == null)
            {
                MessageBox.Show("Select a staff member from the list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var request = new ApiClient.UpdateStaffRequest
            {
                Username = txtUsername.Text.Trim(),
                RoleName = cmbRole.SelectedItem?.ToString() ?? "",
                FullName = txtFullName.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                BranchId = cmbBranch.SelectedValue as int?,
                IsActive = chkActive.Checked,
            };

            try
            {
                await _api.UpdateStaffAsync(_selectedUserId.Value, request);
                MessageBox.Show("Staff updated.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                await LoadStaffAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not update staff: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task DeactivateStaffAsync()
        {
            if (_selectedUserId == null)
            {
                MessageBox.Show("Select a staff member from the list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("Deactivate this staff account?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                await _api.DeactivateStaffAsync(_selectedUserId.Value);
                ClearForm();
                await LoadStaffAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not deactivate staff: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task ResetPasswordAsync()
        {
            if (_selectedUserId == null)
            {
                MessageBox.Show("Select a staff member first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var newPassword = PromptForPassword();
            if (string.IsNullOrEmpty(newPassword)) return;

            try
            {
                await _api.ResetStaffPasswordAsync(_selectedUserId.Value, newPassword);
                MessageBox.Show("Password reset.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not reset password: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string? PromptForPassword()
        {
            using var dlg = new Form
            {
                Text = "Reset Password",
                Size = new Size(360, 160),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
            };
            var lbl = new Label { Text = "New password (min 6 chars):", Location = new Point(20, 20), AutoSize = true };
            var txt = new TextBox { Location = new Point(20, 45), Width = 300, UseSystemPasswordChar = true };
            var btnOk = new Button { Text = "OK", DialogResult = DialogResult.OK, Location = new Point(160, 80), Width = 75 };
            var btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(245, 80), Width = 75 };

            dlg.Controls.AddRange(new Control[] { lbl, txt, btnOk, btnCancel });
            dlg.AcceptButton = btnOk;
            dlg.CancelButton = btnCancel;

            return dlg.ShowDialog(this) == DialogResult.OK ? txt.Text : null;
        }

        // ------------------------------------------------------------------
        // Form helpers
        // ------------------------------------------------------------------

        private bool ValidateAddForm()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Username, password, and full name are required.", "Missing Fields",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtPassword.Text.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters.", "Password Too Short",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbRole.SelectedItem == null)
            {
                MessageBox.Show("Select a role.", "Missing Role",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbBranch.SelectedValue == null)
            {
                MessageBox.Show("Select a branch.", "Missing Branch",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            _selectedUserId = null;
            txtUsername.Clear();
            txtUsername.ReadOnly = false;
            txtUsername.BackColor = Color.White;
            txtFullName.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            cmbRole.Enabled = true;
            if (cmbRole.Items.Count > 0) cmbRole.SelectedIndex = 0;
            chkActive.Checked = true;
            dgvStaff.ClearSelection();
            txtUsername.Focus();
        }

        // Lazy-load branches the first time this control becomes visible.
        
    }
}