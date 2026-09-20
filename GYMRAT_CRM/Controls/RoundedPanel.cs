using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CRM.winforms.Controls
{
    public class RoundedPanel : Panel
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CornerRadius { get; set; } = 14;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color BorderColor { get; set; } = Color.FromArgb(230, 230, 235);

        public RoundedPanel()
        {
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            BackColor = Color.White;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using var path = RoundedRect(rect, CornerRadius);
            Region = new Region(path);
            using var pen = new Pen(BorderColor);
            e.Graphics.DrawPath(pen, path);
        }

        private static GraphicsPath RoundedRect(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}