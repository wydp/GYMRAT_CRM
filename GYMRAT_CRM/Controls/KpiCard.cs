using System;
using System.Drawing;
using System.Windows.Forms;

namespace CRM.winforms.Controls
{
    // A small "KPI card" — the boxy tile with a title, a big number, and an
    // optional subtitle, used at the top of each report tab.
    public class KpiCard : Panel
    {
        private readonly Label _lblTitle;
        private readonly Label _lblValue;
        private readonly Label _lblSub;

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string Title
        {
            get => _lblTitle.Text;
            set => _lblTitle.Text = value;
        }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string Value
        {
            get => _lblValue.Text;
            set => _lblValue.Text = value;
        }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string Subtitle
        {
            get => _lblSub.Text;
            set => _lblSub.Text = value;
        }

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public Color ValueColor
        {
            get => _lblValue.ForeColor;
            set => _lblValue.ForeColor = value;
        }

        public KpiCard()
        {
            BackColor = Color.White;
            Size = new Size(180, 90);
            Padding = new Padding(0);

            _lblTitle = new Label
            {
                Text = "Title",
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(12, 10),
                Size = new Size(156, 16),
                AutoSize = false,
            };

            _lblValue = new Label
            {
                Text = "0",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(10, 28),
                Size = new Size(170, 38),
                AutoSize = false,
            };

            _lblSub = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 8F),
                ForeColor = Color.FromArgb(156, 163, 175),
                Location = new Point(12, 66),
                Size = new Size(156, 16),
                AutoSize = false,
            };

            Controls.Add(_lblTitle);
            Controls.Add(_lblValue);
            Controls.Add(_lblSub);

            // Thin border painted around the panel (Panel doesn't have one by default)
            Paint += KpiCard_Paint;
        }

        private void KpiCard_Paint(object? sender, PaintEventArgs e)
        {
            using var pen = new Pen(Color.FromArgb(229, 231, 235));
            e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
        }
    }
}