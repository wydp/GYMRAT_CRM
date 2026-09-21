using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CRM.winforms.Controls;
using CRM.winforms.Model;

namespace CRM.winforms
{
    public partial class Form1 : Form
    {
        // TEMP: hardcoded until a login screen exists.
        // Later, replace these two lines with real values from login.
        private string currentRole = "Admin";
        private string currentUserName = "Moni Roy";

        // Palette instances — same classes you already have in Model/
        private readonly Shades shades = new Shades();
        private readonly Tints tints = new Tints();
        private readonly Tones tones = new Tones();
        private readonly AccentColors accents = new AccentColors();

        private readonly ContextMenuStrip profileMenu = new ContextMenuStrip();
        private Button? activeNavButton;

        // Which sidebar items each role can see.
        // Source: GymRat use case doc, "User Access" section.
        // NOTE: "Customers" and "Membership Plans" aren't named modules in the use
        // case doc — they'd formally live under "Sales Force Automation" — but
        // they're the only fully working entities right now, so they're listed
        // as their own temporary items until Sales Force Automation is built out.
        private static readonly Dictionary<string, string[]> RoleModules = new Dictionary<string, string[]>
        {
            ["Super Admin"] = new[] { "Software Subscription", "Terms & Conditions", "Subscription Analytics" },
            ["Admin"] = new[] { "Dashboard", "Customers", "Membership Plans", "Branch Management", "Customer Support", "Marketing Automation", "Sales Force Automation", "Terms & Conditions" },
            ["Manager"] = new[] { "Dashboard", "Customers", "Membership Plans", "Customer Support", "Marketing Automation", "Sales Force Automation", "Terms & Conditions" },
            ["Staff"] = new[] { "Customers", "Customer Support", "Sales Force Automation", "Terms & Conditions" },
        };

        public Form1()
        {
            InitializeComponent();
            ApplyPalette();
            SetupProfileMenu();
            BuildSidebar();
        }

        // Applies palette colors to the parts of the shell that don't
        // change per-module (logo, content background). Sidebar button
        // colors are set separately in BuildSidebar/SetActiveButton.
        private void ApplyPalette()
        {
            logoLabel.ForeColor = Color.FromArgb(72, 128, 255); // Primary
            contentPanel.BackColor = Color.FromArgb(245, 246, 250); // Background gray
        }

        // Builds the right-click-style dropdown for the profile label.
        // ContextMenuStrip is the standard WinForms control for this —
        // not a hack, just won't look as rounded/styled as the reference image.
        private void SetupProfileMenu()
        {
            profileMenu.Items.Add("Profile", null, (s, e) =>
                MessageBox.Show("Profile screen not built yet."));
            profileMenu.Items.Add("Settings", null, (s, e) =>
                MessageBox.Show("Settings screen not built yet."));
            profileMenu.Items.Add(new ToolStripSeparator());
            profileMenu.Items.Add("Sign Out", null, (s, e) =>
                MessageBox.Show("Sign out will be wired up once login is built."));

            profileLabel.Text = $"{currentUserName} \u25BE";
            profileLabel.Click += (s, e) =>
                profileMenu.Show(profileLabel, new Point(0, profileLabel.Height));
        }

        // Reads RoleModules for currentRole and generates one Button per
        // module name. Rebuilding from scratch (Controls.Clear) instead of
        // show/hide keeps this simple for now — fine at this scale (≤8 items).
        private void BuildSidebar()
        {
            sidebarPanel.Controls.Clear();

            var modules = RoleModules.ContainsKey(currentRole)
                ? RoleModules[currentRole]
                : Array.Empty<string>();

            int y = 20;
            foreach (var moduleName in modules)
            {
                var btn = new Button
                {
                    Text = moduleName,
                    Tag = moduleName, // stores the module name so the click handler knows which one was pressed
                    FlatStyle = FlatStyle.Flat,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(16, 0, 0, 0),
                    Location = new Point(0, y),
                    Size = new Size(sidebarPanel.Width, 44),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    BackColor = Color.White,
                    ForeColor = Color.FromArgb(31, 41, 55), // Text Primary
                    Font = new Font("Segoe UI", 10F),
                    Cursor = Cursors.Hand,
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.Click += NavButton_Click;

                sidebarPanel.Controls.Add(btn);
                y += 44;
            }

            if (sidebarPanel.Controls.Count > 0)
                SetActiveButton((Button)sidebarPanel.Controls[0]);
        }

        private void NavButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button clicked) return; // guards the null/cast in one step
            SetActiveButton(clicked);
            LoadModule(clicked.Tag?.ToString() ?? string.Empty);
        }

        // Resets the previously active button back to white/normal,
        // then paints the newly clicked one with the Primary blue —
        // this is what gives the "Dashboard" item its highlighted look
        // in the reference screenshot.
        private void SetActiveButton(Button button)
        {
            if (activeNavButton != null)
            {
                activeNavButton.BackColor = Color.White;
                activeNavButton.ForeColor = Color.FromArgb(31, 41, 55);
            }

            button.BackColor = Color.FromArgb(72, 128, 255); // Primary
            button.ForeColor = Color.White;
            activeNavButton = button;
        }

        // Swaps the visible content. Real modules get their own UserControl
        // added as a case below; anything not built yet falls through to
        // the placeholder label.
        private void LoadModule(string moduleName)
        {
            contentPanel.Controls.Clear();

            UserControl moduleControl = moduleName switch
            {
                "Dashboard" => new DashboardControl { Dock = DockStyle.Fill },
                "Customers" => new CustomerControl { Dock = DockStyle.Fill },
                "Membership Plans" => new MembershipPlanControl { Dock = DockStyle.Fill },
                "Customer Support" => new CustomerSupportControl { Dock = DockStyle.Fill },
                _ => null,
            };

            if (moduleControl != null)
            {
                contentPanel.Controls.Add(moduleControl);
                return;
            }

            var placeholder = new Label
            {
                Text = $"{moduleName} module \u2014 not built yet.",
                Font = new Font("Segoe UI", 14F),
                ForeColor = Color.FromArgb(107, 114, 128), // Text Secondary
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
            };
            contentPanel.Controls.Add(placeholder);
        }
    }
}