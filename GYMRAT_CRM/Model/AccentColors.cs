using System.Drawing;

namespace CRM.winforms.Model
{
    internal class AccentColors
    {
        // Success — Add button
        public Color Success = Color.FromArgb(34, 197, 94);
        public Color SuccessDark = Color.FromArgb(21, 128, 61);
        public Color SuccessLight = Color.FromArgb(220, 252, 231);

        // Warning — Deactivate button
        public Color Warning = Color.FromArgb(245, 158, 11);
        public Color WarningDark = Color.FromArgb(180, 111, 9);
        public Color WarningLight = Color.FromArgb(254, 243, 199);

        // Danger — Delete (new)
        public Color Danger = Color.FromArgb(239, 68, 68);
        public Color DangerDark = Color.FromArgb(185, 28, 28);
        public Color DangerLight = Color.FromArgb(254, 226, 226);

        // Neutral/Gray — Refresh/Clear button
        public Color Neutral = Color.FromArgb(156, 163, 175);
        public Color NeutralDark = Color.FromArgb(107, 114, 128);
        public Color NeutralLight = Color.FromArgb(243, 244, 246);
    }
}