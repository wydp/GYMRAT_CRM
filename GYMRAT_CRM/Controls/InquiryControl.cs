using CRM.domain.Entities;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CRM.winforms.Controls
{
    public partial class InquiryControl : UserControl
    {
        private readonly ApiClient _api = new ApiClient();
        private int? _selectedInquiryId = null;

        public InquiryControl()
        {
            InitializeComponent();
            StyleGrid();
            cboStatus.Items.AddRange(Enum.GetNames(typeof(RequestStatus)));
            WireEvents();
            _ = LoadAllAsync();
        }

        private void StyleGrid()
        {
            dgvInquiries.BorderStyle = BorderStyle.None;
            dgvInquiries.RowHeadersVisible = false;
            dgvInquiries.AllowUserToAddRows = false;
            dgvInquiries.AllowUserToDeleteRows = false;
            dgvInquiries.ReadOnly = true;
            dgvInquiries.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInquiries.MultiSelect = false;
            dgvInquiries.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInquiries.EnableHeadersVisualStyles = false;
            dgvInquiries.GridColor = Color.FromArgb(229, 231, 235);
            dgvInquiries.ColumnHeadersHeight = 40;
            dgvInquiries.RowTemplate.Height = 36;

            dgvInquiries.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237, 242, 255);
            dgvInquiries.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvInquiries.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            dgvInquiries.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55); // Text Primary — explicit, since dark-mode defaults can render invisible-on-white
            dgvInquiries.DefaultCellStyle.SelectionBackColor = Color.FromArgb(72, 128, 255);
            dgvInquiries.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvInquiries.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvInquiries.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);

            dgvInquiries.Columns.Add("Customer", "Customer");
            dgvInquiries.Columns.Add("Subject", "Subject");
            dgvInquiries.Columns.Add("Message", "Message");
            dgvInquiries.Columns.Add("Status", "Status");
        }

        private void WireEvents()
        {
            btnAdd.Click += async (s, e) => await AddInquiryAsync();
            btnUpdateStatus.Click += async (s, e) => await UpdateStatusAsync();
            btnDeactivate.Click += async (s, e) => await DeactivateAsync();
            btnRefresh.Click += async (s, e) => { ClearForm(); await LoadAllAsync(); };
            dgvInquiries.SelectionChanged += DgvInquiries_SelectionChanged;
        }

        // Loads both the customer dropdown and the inquiry grid.
        // Only active customers are offered — inactive ones shouldn't be
        // filing new inquiries, though their past ones still show in the grid.
        private async System.Threading.Tasks.Task LoadAllAsync()
        {
            try
            {
                var customers = await _api.GetCustomersAsync();
                cboCustomer.DataSource = customers.Where(c => c.IsActive).OrderBy(c => c.CustomerName).ToList();
                cboCustomer.DisplayMember = "CustomerName";
                cboCustomer.ValueMember = "CustomerId";

                var inquiries = await _api.GetInquiriesAsync();
                dgvInquiries.Rows.Clear();

                foreach (var i in inquiries.OrderBy(x => x.InquiryId))
                {
                    // Tag set BEFORE Rows.Add — see CustomerControl for why.
                    var row = new DataGridViewRow();
                    row.CreateCells(dgvInquiries,
                        i.Customer?.CustomerName ?? $"#{i.CustomerId}",
                        i.Subject, i.Message, i.Status.ToString());
                    row.Tag = i.InquiryId;

                    dgvInquiries.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load inquiries: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvInquiries_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvInquiries.SelectedRows.Count == 0) return;

            var row = dgvInquiries.SelectedRows[0];
            if (row.Tag is not int inquiryId) return; // defensive: Tag not set yet
            _selectedInquiryId = inquiryId;
            cboStatus.SelectedItem = row.Cells["Status"].Value?.ToString();
        }

        private async System.Threading.Tasks.Task AddInquiryAsync()
        {
            if (cboCustomer.SelectedValue == null || string.IsNullOrWhiteSpace(txtSubject.Text)
                || string.IsNullOrWhiteSpace(txtMessage.Text))
            {
                MessageBox.Show("Customer, Subject, and Message are required.", "Missing Fields",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var inquiry = new Inquiry
            {
                CustomerId = (int)cboCustomer.SelectedValue,
                Subject = txtSubject.Text.Trim(),
                Message = txtMessage.Text.Trim(),
            };

            try
            {
                await _api.CreateInquiryAsync(inquiry);
                ClearForm();
                await LoadAllAsync();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Could Not Create Inquiry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async System.Threading.Tasks.Task UpdateStatusAsync()
        {
            if (_selectedInquiryId == null)
            {
                MessageBox.Show("Select an inquiry from the list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboStatus.SelectedItem == null)
            {
                MessageBox.Show("Choose a status first.", "No Status Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var status = Enum.Parse<RequestStatus>(cboStatus.SelectedItem.ToString()!);
            await _api.UpdateInquiryStatusAsync(_selectedInquiryId.Value, status);
            ClearForm();
            await LoadAllAsync();
        }

        private async System.Threading.Tasks.Task DeactivateAsync()
        {
            if (_selectedInquiryId == null)
            {
                MessageBox.Show("Select an inquiry from the list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("Deactivate this inquiry?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            await _api.DeactivateInquiryAsync(_selectedInquiryId.Value);
            ClearForm();
            await LoadAllAsync();
        }

        private void ClearForm()
        {
            _selectedInquiryId = null;
            if (cboCustomer.Items.Count > 0) cboCustomer.SelectedIndex = -1;
            txtSubject.Clear();
            txtMessage.Clear();
            cboStatus.SelectedIndex = -1;
            dgvInquiries.ClearSelection();
        }
    }
}