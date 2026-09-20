using CRM.domain.Entities;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CRM.winforms.Controls
{
    public partial class CustomerControl : UserControl
    {
        private readonly ApiClient _api = new ApiClient();
        private int? _selectedCustomerId = null; // null = "Add" mode, set = "Update/Deactivate" mode

        public CustomerControl()
        {
            InitializeComponent();
            StyleGrid();
            WireEvents();
            _ = LoadCustomersAsync();
        }

        // Applies the palette to the grid — same colors used in Form1's shell,
        // kept here instead of Designer since color logic is easier to read as code.
        private void StyleGrid()
        {
            dgvCustomers.BorderStyle = BorderStyle.None;
            dgvCustomers.RowHeadersVisible = false;
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.ReadOnly = true;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.MultiSelect = false;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.EnableHeadersVisualStyles = false;
            dgvCustomers.GridColor = Color.FromArgb(229, 231, 235); // Border
            dgvCustomers.ColumnHeadersHeight = 40;
            dgvCustomers.RowTemplate.Height = 36;

            dgvCustomers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237, 242, 255); // Tints.AlmostWhiteBlue
            dgvCustomers.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvCustomers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            dgvCustomers.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55); // Text Primary — explicit, since dark-mode defaults can render invisible-on-white
            dgvCustomers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(72, 128, 255); // Primary
            dgvCustomers.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvCustomers.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvCustomers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);

            dgvCustomers.Columns.Add("CustomerCode", "Code");
            dgvCustomers.Columns.Add("CustomerName", "Name");
            dgvCustomers.Columns.Add("ContactNumber", "Contact");
            dgvCustomers.Columns.Add("EmailAddress", "Email");
            dgvCustomers.Columns.Add("Address", "Address");
            dgvCustomers.Columns.Add("Status", "Status");
        }

        private void WireEvents()
        {
            btnAdd.Click += async (s, e) => await AddCustomerAsync();
            btnUpdate.Click += async (s, e) => await UpdateCustomerAsync();
            btnDeactivate.Click += async (s, e) => await DeactivateCustomerAsync();
            btnRefresh.Click += async (s, e) => { ClearForm(); await LoadCustomersAsync(); };
            dgvCustomers.SelectionChanged += DgvCustomers_SelectionChanged;
        }

        private async System.Threading.Tasks.Task LoadCustomersAsync()
        {
            try
            {
                var customers = await _api.GetCustomersAsync();
                dgvCustomers.Rows.Clear();

                foreach (var c in customers.OrderBy(x => x.CustomerId))
                {
                    // Build the row and set Tag BEFORE adding it to the grid.
                    // Rows.Add() fires SelectionChanged synchronously for the
                    // first row added to an empty grid — setting Tag afterward
                    // is too late and the handler reads null, causing a crash.
                    var row = new DataGridViewRow();
                    row.CreateCells(dgvCustomers,
                        c.CustomerCode, c.CustomerName, c.ContactNumber,
                        c.EmailAddress, c.Address, c.IsActive ? "Active" : "Inactive");
                    row.Tag = c.CustomerId; // must be set before Add — see comment above

                    int rowIndex = dgvCustomers.Rows.Add(row);
                    // Cells["Status"] by NAME only resolves once the row is attached
                    // to the grid (needs row.DataGridView set) — so this has to come
                    // AFTER Add, unlike Tag.
                    dgvCustomers.Rows[rowIndex].Cells["Status"].Style.ForeColor =
                        c.IsActive ? Color.FromArgb(21, 128, 61) : Color.FromArgb(185, 28, 28);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load customers: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0) return;

            var row = dgvCustomers.SelectedRows[0];
            if (row.Tag is not int customerId) return; // defensive: Tag not set yet
            _selectedCustomerId = customerId;

            txtCode.Text = row.Cells["CustomerCode"].Value?.ToString();
            txtName.Text = row.Cells["CustomerName"].Value?.ToString();
            txtContact.Text = row.Cells["ContactNumber"].Value?.ToString();
            txtEmail.Text = row.Cells["EmailAddress"].Value?.ToString();
            txtAddress.Text = row.Cells["Address"].Value?.ToString();
            chkActive.Checked = row.Cells["Status"].Value?.ToString() == "Active";
        }

        private async System.Threading.Tasks.Task AddCustomerAsync()
        {
            if (!ValidateForm()) return;

            try
            {
                await _api.CreateCustomerAsync(BuildCustomerFromForm());
                ClearForm();
                await LoadCustomersAsync();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Duplicate Code", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async System.Threading.Tasks.Task UpdateCustomerAsync()
        {
            if (_selectedCustomerId == null)
            {
                MessageBox.Show("Select a customer from the list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!ValidateForm()) return;

            try
            {
                var customer = BuildCustomerFromForm();
                customer.CustomerId = _selectedCustomerId.Value;
                await _api.UpdateCustomerAsync(_selectedCustomerId.Value, customer);
                ClearForm();
                await LoadCustomersAsync();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Duplicate Code", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async System.Threading.Tasks.Task DeactivateCustomerAsync()
        {
            if (_selectedCustomerId == null)
            {
                MessageBox.Show("Select a customer from the list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("Deactivate this customer?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            await _api.DeactivateCustomerAsync(_selectedCustomerId.Value);
            ClearForm();
            await LoadCustomersAsync();
        }

        private Customer BuildCustomerFromForm() => new Customer
        {
            CustomerCode = txtCode.Text.Trim(),
            CustomerName = txtName.Text.Trim(),
            ContactNumber = string.IsNullOrWhiteSpace(txtContact.Text) ? null : txtContact.Text.Trim(),
            EmailAddress = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
            Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim(),
            IsActive = chkActive.Checked,
        };

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Customer Code and Customer Name are required.", "Missing Fields",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void ClearForm()
        {
            _selectedCustomerId = null;
            txtCode.Clear();
            txtName.Clear();
            txtContact.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            chkActive.Checked = true;
            dgvCustomers.ClearSelection();
        }
    }
}