using System;
using System.Drawing;
using System.Windows.Forms;
using CRM.domain.Entities;
using CRM.winforms.Model;

namespace CRM.winforms
{
    public partial class Form1 : Form
    {
        private readonly ApiClient _api = new ApiClient();
        private readonly Tints _tints = new Tints();
        private readonly Shades _shades = new Shades();

        // Customers tab
        private DataGridView dgvCustomers;
        private TextBox txtCustomerCode, txtCustomerName, txtContactNumber, txtEmailAddress, txtAddress;
        private CheckBox chkCustomerActive;
        private int? selectedCustomerId = null;

        // Membership Plans tab
        private DataGridView dgvPlans;
        private TextBox txtPlanCode, txtPlanName, txtDescription;
        private NumericUpDown numPrice, numDuration;
        private CheckBox chkPlanActive;
        private int? selectedPlanId = null;

        public Form1()
        {
            InitializeComponent();
            BuildUi();
        }

        private void BuildUi()
        {
            this.Text = "GymRat CRM";
            this.Width = 950;
            this.Height = 650;

            var tabs = new TabControl { Dock = DockStyle.Fill };
            var tabCustomers = new TabPage("Customers");
            var tabPlans = new TabPage("Membership Plans");
            tabs.TabPages.Add(tabCustomers);
            tabs.TabPages.Add(tabPlans);
            this.Controls.Add(tabs);

            BuildCustomersTab(tabCustomers);
            BuildPlansTab(tabPlans);

            this.Load += async (s, e) =>
            {
                await LoadCustomersAsync();
                await LoadPlansAsync();
            };
        }

        // ===================== CUSTOMERS TAB =====================

        private void BuildCustomersTab(TabPage tab)
        {
            dgvCustomers = new DataGridView
            {
                Location = new Point(10, 10),
                Size = new Size(900, 250),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                ReadOnly = true,
                AutoGenerateColumns = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            dgvCustomers.SelectionChanged += DgvCustomers_SelectionChanged;
            tab.Controls.Add(dgvCustomers);

            var grp = new GroupBox { Text = "Customer Details", Location = new Point(10, 270), Size = new Size(900, 220) };
            txtCustomerCode = AddLabeledTextBox(grp, "Customer Code:", 25);
            txtCustomerName = AddLabeledTextBox(grp, "Customer Name:", 55);
            txtContactNumber = AddLabeledTextBox(grp, "Contact Number:", 85);
            txtEmailAddress = AddLabeledTextBox(grp, "Email Address:", 115);
            txtAddress = AddLabeledTextBox(grp, "Address:", 145);
            chkCustomerActive = new CheckBox { Text = "Active", Location = new Point(140, 175), Checked = true };
            grp.Controls.Add(chkCustomerActive);
            tab.Controls.Add(grp);

            var btnAdd = new Button { Text = "Add", Location = new Point(10, 500), Size = new Size(90, 30), BackColor = _tints.VividRed, ForeColor = Color.White };
            var btnUpdate = new Button { Text = "Update", Location = new Point(110, 500), Size = new Size(90, 30), BackColor = _shades.DarkRed, ForeColor = Color.White };
            var btnDelete = new Button { Text = "Delete", Location = new Point(210, 500), Size = new Size(90, 30), BackColor = _shades.Maroon, ForeColor = Color.White };
            var btnRefresh = new Button { Text = "Refresh", Location = new Point(310, 500), Size = new Size(90, 30) };
            var btnClear = new Button { Text = "Clear", Location = new Point(410, 500), Size = new Size(90, 30) };

            btnAdd.Click += async (s, e) => await AddCustomerAsync();
            btnUpdate.Click += async (s, e) => await UpdateCustomerAsync();
            btnDelete.Click += async (s, e) => await DeleteCustomerAsync();
            btnRefresh.Click += async (s, e) => await LoadCustomersAsync();
            btnClear.Click += (s, e) => ClearCustomerFields();

            tab.Controls.Add(btnAdd);
            tab.Controls.Add(btnUpdate);
            tab.Controls.Add(btnDelete);
            tab.Controls.Add(btnRefresh);
            tab.Controls.Add(btnClear);
        }

        private void DgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow?.DataBoundItem is not Customer c) return;

            selectedCustomerId = c.CustomerId;
            txtCustomerCode.Text = c.CustomerCode;
            txtCustomerName.Text = c.CustomerName;
            txtContactNumber.Text = c.ContactNumber;
            txtEmailAddress.Text = c.EmailAddress;
            txtAddress.Text = c.Address;
            chkCustomerActive.Checked = c.IsActive;
        }

        private async System.Threading.Tasks.Task LoadCustomersAsync()
        {
            try
            {
                var customers = await _api.GetCustomersAsync();
                dgvCustomers.DataSource = customers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load customers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateCustomerFields()
        {
            if (string.IsNullOrWhiteSpace(txtCustomerCode.Text) || string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                MessageBox.Show("Customer Code and Customer Name are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private async System.Threading.Tasks.Task AddCustomerAsync()
        {
            if (!ValidateCustomerFields()) return;

            var customer = new Customer
            {
                CustomerCode = txtCustomerCode.Text.Trim(),
                CustomerName = txtCustomerName.Text.Trim(),
                ContactNumber = txtContactNumber.Text.Trim(),
                EmailAddress = txtEmailAddress.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                IsActive = chkCustomerActive.Checked
            };

            try
            {
                await _api.CreateCustomerAsync(customer);
                await LoadCustomersAsync();
                ClearCustomerFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task UpdateCustomerAsync()
        {
            if (selectedCustomerId is null)
            {
                MessageBox.Show("Select a customer from the grid first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateCustomerFields()) return;

            var customer = new Customer
            {
                CustomerCode = txtCustomerCode.Text.Trim(),
                CustomerName = txtCustomerName.Text.Trim(),
                ContactNumber = txtContactNumber.Text.Trim(),
                EmailAddress = txtEmailAddress.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                IsActive = chkCustomerActive.Checked
            };

            try
            {
                await _api.UpdateCustomerAsync(selectedCustomerId.Value, customer);
                await LoadCustomersAsync();
                ClearCustomerFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task DeleteCustomerAsync()
        {
            if (selectedCustomerId is null)
            {
                MessageBox.Show("Select a customer from the grid first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Delete this customer?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                await _api.DeleteCustomerAsync(selectedCustomerId.Value);
                await LoadCustomersAsync();
                ClearCustomerFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearCustomerFields()
        {
            selectedCustomerId = null;
            txtCustomerCode.Clear();
            txtCustomerName.Clear();
            txtContactNumber.Clear();
            txtEmailAddress.Clear();
            txtAddress.Clear();
            chkCustomerActive.Checked = true;
        }

        // ===================== MEMBERSHIP PLANS TAB =====================

        private void BuildPlansTab(TabPage tab)
        {
            dgvPlans = new DataGridView
            {
                Location = new Point(10, 10),
                Size = new Size(900, 250),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                ReadOnly = true,
                AutoGenerateColumns = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            dgvPlans.SelectionChanged += DgvPlans_SelectionChanged;
            tab.Controls.Add(dgvPlans);

            var grp = new GroupBox { Text = "Membership Plan Details", Location = new Point(10, 270), Size = new Size(900, 220) };
            txtPlanCode = AddLabeledTextBox(grp, "Plan Code:", 25);
            txtPlanName = AddLabeledTextBox(grp, "Plan Name:", 55);
            txtDescription = AddLabeledTextBox(grp, "Description:", 85);

            var lblPrice = new Label { Text = "Price:", Location = new Point(10, 115), Width = 120 };
            numPrice = new NumericUpDown { Location = new Point(140, 115), Width = 200, DecimalPlaces = 2, Maximum = 999999, Minimum = 0 };
            grp.Controls.Add(lblPrice);
            grp.Controls.Add(numPrice);

            var lblDuration = new Label { Text = "Duration (days):", Location = new Point(10, 145), Width = 120 };
            numDuration = new NumericUpDown { Location = new Point(140, 145), Width = 200, Maximum = 3650, Minimum = 1, Value = 30 };
            grp.Controls.Add(lblDuration);
            grp.Controls.Add(numDuration);

            chkPlanActive = new CheckBox { Text = "Active", Location = new Point(140, 175), Checked = true };
            grp.Controls.Add(chkPlanActive);
            tab.Controls.Add(grp);

            var btnAdd = new Button { Text = "Add", Location = new Point(10, 500), Size = new Size(90, 30), BackColor = _tints.VividRed, ForeColor = Color.White };
            var btnUpdate = new Button { Text = "Update", Location = new Point(110, 500), Size = new Size(90, 30), BackColor = _shades.DarkRed, ForeColor = Color.White };
            var btnDelete = new Button { Text = "Delete", Location = new Point(210, 500), Size = new Size(90, 30), BackColor = _shades.Maroon, ForeColor = Color.White };
            var btnRefresh = new Button { Text = "Refresh", Location = new Point(310, 500), Size = new Size(90, 30) };
            var btnClear = new Button { Text = "Clear", Location = new Point(410, 500), Size = new Size(90, 30) };

            btnAdd.Click += async (s, e) => await AddPlanAsync();
            btnUpdate.Click += async (s, e) => await UpdatePlanAsync();
            btnDelete.Click += async (s, e) => await DeletePlanAsync();
            btnRefresh.Click += async (s, e) => await LoadPlansAsync();
            btnClear.Click += (s, e) => ClearPlanFields();

            tab.Controls.Add(btnAdd);
            tab.Controls.Add(btnUpdate);
            tab.Controls.Add(btnDelete);
            tab.Controls.Add(btnRefresh);
            tab.Controls.Add(btnClear);
        }

        private void DgvPlans_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPlans.CurrentRow?.DataBoundItem is not MembershipPlan p) return;

            selectedPlanId = p.MembershipPlanId;
            txtPlanCode.Text = p.PlanCode;
            txtPlanName.Text = p.PlanName;
            txtDescription.Text = p.Description;
            numPrice.Value = p.Price;
            numDuration.Value = p.DurationInDays;
            chkPlanActive.Checked = p.IsActive;
        }

        private async System.Threading.Tasks.Task LoadPlansAsync()
        {
            try
            {
                var plans = await _api.GetMembershipPlansAsync();
                dgvPlans.DataSource = plans;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load plans: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidatePlanFields()
        {
            if (string.IsNullOrWhiteSpace(txtPlanCode.Text) || string.IsNullOrWhiteSpace(txtPlanName.Text))
            {
                MessageBox.Show("Plan Code and Plan Name are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (numPrice.Value <= 0)
            {
                MessageBox.Show("Price must be greater than 0.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private async System.Threading.Tasks.Task AddPlanAsync()
        {
            if (!ValidatePlanFields()) return;

            var plan = new MembershipPlan
            {
                PlanCode = txtPlanCode.Text.Trim(),
                PlanName = txtPlanName.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                Price = numPrice.Value,
                DurationInDays = (int)numDuration.Value,
                IsActive = chkPlanActive.Checked
            };

            try
            {
                await _api.CreateMembershipPlanAsync(plan);
                await LoadPlansAsync();
                ClearPlanFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add plan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task UpdatePlanAsync()
        {
            if (selectedPlanId is null)
            {
                MessageBox.Show("Select a plan from the grid first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidatePlanFields()) return;

            var plan = new MembershipPlan
            {
                PlanCode = txtPlanCode.Text.Trim(),
                PlanName = txtPlanName.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                Price = numPrice.Value,
                DurationInDays = (int)numDuration.Value,
                IsActive = chkPlanActive.Checked
            };

            try
            {
                await _api.UpdateMembershipPlanAsync(selectedPlanId.Value, plan);
                await LoadPlansAsync();
                ClearPlanFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update plan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async System.Threading.Tasks.Task DeletePlanAsync()
        {
            if (selectedPlanId is null)
            {
                MessageBox.Show("Select a plan from the grid first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Delete this membership plan?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                await _api.DeleteMembershipPlanAsync(selectedPlanId.Value);
                await LoadPlansAsync();
                ClearPlanFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete plan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearPlanFields()
        {
            selectedPlanId = null;
            txtPlanCode.Clear();
            txtPlanName.Clear();
            txtDescription.Clear();
            numPrice.Value = 0;
            numDuration.Value = 30;
            chkPlanActive.Checked = true;
        }

        // ===================== SHARED HELPER =====================

        private TextBox AddLabeledTextBox(Control parent, string labelText, int y)
        {
            var lbl = new Label { Text = labelText, Location = new Point(10, y), Width = 120 };
            var txt = new TextBox { Location = new Point(140, y), Width = 400 };
            parent.Controls.Add(lbl);
            parent.Controls.Add(txt);
            return txt;
        }
    }
}