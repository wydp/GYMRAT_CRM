using CRM.domain.Entities;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CRM.winforms.Controls
{
    public partial class FeedbackControl : UserControl
    {
        private readonly ApiClient _api = new ApiClient();
        private int? _selectedFeedbackId = null;

        public FeedbackControl()
        {
            InitializeComponent();
            StyleGrid();
            cboType.Items.AddRange(Enum.GetNames(typeof(FeedbackType)));
            cboStatus.Items.AddRange(Enum.GetNames(typeof(RequestStatus)));
            WireEvents();
            _ = LoadAllAsync();
        }

        private void StyleGrid()
        {
            dgvFeedback.BorderStyle = BorderStyle.None;
            dgvFeedback.RowHeadersVisible = false;
            dgvFeedback.AllowUserToAddRows = false;
            dgvFeedback.AllowUserToDeleteRows = false;
            dgvFeedback.ReadOnly = true;
            dgvFeedback.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFeedback.MultiSelect = false;
            dgvFeedback.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFeedback.EnableHeadersVisualStyles = false;
            dgvFeedback.GridColor = Color.FromArgb(229, 231, 235);
            dgvFeedback.ColumnHeadersHeight = 40;
            dgvFeedback.RowTemplate.Height = 36;

            dgvFeedback.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(237, 242, 255);
            dgvFeedback.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvFeedback.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            dgvFeedback.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55); // Text Primary — explicit, since dark-mode defaults can render invisible-on-white
            dgvFeedback.DefaultCellStyle.SelectionBackColor = Color.FromArgb(72, 128, 255);
            dgvFeedback.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvFeedback.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvFeedback.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);

            dgvFeedback.Columns.Add("Customer", "Customer");
            dgvFeedback.Columns.Add("Type", "Type");
            dgvFeedback.Columns.Add("Message", "Message");
            dgvFeedback.Columns.Add("Status", "Status");
        }

        private void WireEvents()
        {
            btnAdd.Click += async (s, e) => await AddFeedbackAsync();
            btnUpdateStatus.Click += async (s, e) => await UpdateStatusAsync();
            btnDeactivate.Click += async (s, e) => await DeactivateAsync();
            btnRefresh.Click += async (s, e) => { ClearForm(); await LoadAllAsync(); };
            dgvFeedback.SelectionChanged += DgvFeedback_SelectionChanged;
        }

        private async System.Threading.Tasks.Task LoadAllAsync()
        {
            try
            {
                var customers = await _api.GetCustomersAsync();
                cboCustomer.DataSource = customers.Where(c => c.IsActive).OrderBy(c => c.CustomerName).ToList();
                cboCustomer.DisplayMember = "CustomerName";
                cboCustomer.ValueMember = "CustomerId";

                var feedbacks = await _api.GetFeedbacksAsync();
                dgvFeedback.Rows.Clear();

                foreach (var f in feedbacks.OrderBy(x => x.FeedbackId))
                {
                    // Tag set BEFORE Rows.Add — see CustomerControl for why.
                    var row = new DataGridViewRow();
                    row.CreateCells(dgvFeedback,
                        f.Customer?.CustomerName ?? $"#{f.CustomerId}",
                        f.Type.ToString(), f.Message, f.Status.ToString());
                    row.Tag = f.FeedbackId; // must be set before Add — see CustomerControl

                    int rowIndex = dgvFeedback.Rows.Add(row);
                    // Cells[...] by NAME needs row.DataGridView set — after Add only.
                    if (f.Type == FeedbackType.Complaint)
                        dgvFeedback.Rows[rowIndex].Cells["Type"].Style.ForeColor = Color.FromArgb(185, 28, 28);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load feedback: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvFeedback_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvFeedback.SelectedRows.Count == 0) return;

            var row = dgvFeedback.SelectedRows[0];
            if (row.Tag is not int feedbackId) return; // defensive: Tag not set yet
            _selectedFeedbackId = feedbackId;
            cboStatus.SelectedItem = row.Cells["Status"].Value?.ToString();
        }

        private async System.Threading.Tasks.Task AddFeedbackAsync()
        {
            if (cboCustomer.SelectedValue == null || cboType.SelectedItem == null
                || string.IsNullOrWhiteSpace(txtMessage.Text))
            {
                MessageBox.Show("Customer, Type, and Message are required.", "Missing Fields",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var feedback = new Feedback
            {
                CustomerId = (int)cboCustomer.SelectedValue,
                Type = Enum.Parse<FeedbackType>(cboType.SelectedItem.ToString()!),
                Message = txtMessage.Text.Trim(),
            };

            try
            {
                await _api.CreateFeedbackAsync(feedback);
                ClearForm();
                await LoadAllAsync();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Could Not Submit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async System.Threading.Tasks.Task UpdateStatusAsync()
        {
            if (_selectedFeedbackId == null)
            {
                MessageBox.Show("Select a feedback entry from the list first.", "No Selection",
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
            await _api.UpdateFeedbackStatusAsync(_selectedFeedbackId.Value, status);
            ClearForm();
            await LoadAllAsync();
        }

        private async System.Threading.Tasks.Task DeactivateAsync()
        {
            if (_selectedFeedbackId == null)
            {
                MessageBox.Show("Select a feedback entry from the list first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show("Deactivate this feedback entry?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            await _api.DeactivateFeedbackAsync(_selectedFeedbackId.Value);
            ClearForm();
            await LoadAllAsync();
        }

        private void ClearForm()
        {
            _selectedFeedbackId = null;
            if (cboCustomer.Items.Count > 0) cboCustomer.SelectedIndex = -1;
            cboType.SelectedIndex = -1;
            txtMessage.Clear();
            cboStatus.SelectedIndex = -1;
            dgvFeedback.ClearSelection();
        }
    }
}