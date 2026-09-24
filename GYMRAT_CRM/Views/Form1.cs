using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CRM.winforms.Controls;
using CRM.winforms.Model;

namespace CRM.winforms
{
    public partial class Form1 : Form
    {
        private string currentRole => AuthContext.RoleName;
        private string currentUserName => AuthContext.FullName;

        // Palette instances — same classes you already have in Model/
        private readonly Shades shades = new Shades();
        private readonly Tints tints = new Tints();
        private readonly Tones tones = new Tones();
        private readonly AccentColors accents = new AccentColors();

        private readonly ContextMenuStrip profileMenu = new ContextMenuStrip();
        private Button? activeNavButton;

        // Sidebar structure — grouped sections, each with a list of modules.
        // A module only appears if the logged-in user has the required permission.
        // A section header only appears if at least one module under it is visible.
        private static readonly (string Section, (string Label, string Permission)[] Modules)[] SidebarStructure = new[]
        {
            ("MAIN", new[]
            {
                ("Dashboard",                "dashboard.access"),
                ("Reports",                  "reports.access"),
            }),
            ("OPERATIONS", new[]
            {
                ("Customers",                "sfa.access"),
                ("Membership Plans",         "sfa.access"),
                ("Sales Force Automation",   "sfa.access"),
                ("Retention",                "retention.access"),
            }),
            ("SUPPORT", new[]
            {
                ("Customer Support",         "support.access"),
                ("Communication",            "communication.access"),
            }),
            ("MARKETING", new[]
            {
                ("Marketing Automation",     "marketing.access"),
            }),
            ("ADMINISTRATION", new[]
            {
                ("Branch Management",        "branch.access"),
                ("Staff Management",         "staff.access"),
                ("Audit Log",                "audit.access"),
            }),
            ("SYSTEM", new[]
            {
                ("Terms & Conditions",       "terms.access"),
                ("Platform Management",      "platform.access"),
            }),
        };

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            ApplyPalette();
            SetupProfileMenu();
            BuildSidebar();
        }

        private void ApplyPalette()
        {
            logoLabel.ForeColor = Color.FromArgb(72, 128, 255);
            contentPanel.BackColor = Color.FromArgb(245, 246, 250);
        }

        private void SetupProfileMenu()
        {
            profileMenu.Items.Add("Profile", null, (s, e) =>
                MessageBox.Show("Profile screen not built yet."));
            profileMenu.Items.Add("Settings", null, (s, e) =>
                MessageBox.Show("Settings screen not built yet."));
            profileMenu.Items.Add(new ToolStripSeparator());
            profileMenu.Items.Add("Sign Out", null, (s, e) => SignOut());

            profileLabel.Text = $"{currentUserName} \u25BE";
            profileLabel.Click += (s, e) =>
                profileMenu.Show(profileLabel, new Point(0, profileLabel.Height));
        }

        private void SignOut()
        {
            var confirm = MessageBox.Show("Sign out?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            AuthContext.Clear();

            var login = new LoginForm();
            login.Show();
            this.Close();
        }

        // Builds the sidebar from SidebarStructure, filtered by the current
        // user's permissions (from AuthContext). Sections with no visible
        // modules are skipped entirely — no empty headers, no dividers.
        private void BuildSidebar()
        {
            sidebarPanel.Controls.Clear();

            const int buttonHeight = 40;
            const int sectionHeaderHeight = 28;
            const int leftPadding = 16;
            const int sectionTopGap = 10;

            int y = 16;
            bool isFirstSection = true;

            foreach (var (sectionName, modules) in SidebarStructure)
            {
                // Filter to modules the current user has permission for
                var visible = modules
                    .Where(m => AuthContext.HasPermission(m.Permission))
                    .ToList();

                if (visible.Count == 0) continue;

                // Divider line above every section except the first
                if (!isFirstSection)
                {
                    var divider = new Panel
                    {
                        BackColor = Color.FromArgb(229, 231, 235),
                        Location = new Point(0, y),
                        Size = new Size(sidebarPanel.Width, 1),
                        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    };
                    sidebarPanel.Controls.Add(divider);
                    y += 1 + sectionTopGap;
                }

                // Section header label
                var header = new Label
                {
                    Text = sectionName,
                    Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(156, 163, 175),
                    Location = new Point(leftPadding, y),
                    Size = new Size(sidebarPanel.Width, sectionHeaderHeight),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                };
                sidebarPanel.Controls.Add(header);
                y += sectionHeaderHeight;

                // Module buttons
                foreach (var (label, permission) in visible)
                {
                    var btn = new Button
                    {
                        Text = label,
                        Tag = label,
                        FlatStyle = FlatStyle.Flat,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Padding = new Padding(leftPadding, 0, 0, 0),
                        Location = new Point(0, y),
                        Size = new Size(sidebarPanel.Width, buttonHeight),
                        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                        BackColor = Color.White,
                        ForeColor = Color.FromArgb(31, 41, 55),
                        Font = new Font("Segoe UI", 10F),
                        Cursor = Cursors.Hand,
                    };
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += NavButton_Click;

                    sidebarPanel.Controls.Add(btn);
                    y += buttonHeight;
                }

                isFirstSection = false;
            }

            // Auto-load the first visible module on startup
            var firstButton = sidebarPanel.Controls.OfType<Button>().FirstOrDefault();
            if (firstButton != null)
            {
                SetActiveButton(firstButton);
                LoadModule(firstButton.Tag?.ToString() ?? "Dashboard");
            }
        }

        private void NavButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button clicked) return;
            SetActiveButton(clicked);
            LoadModule(clicked.Tag?.ToString() ?? string.Empty);
        }

        private void SetActiveButton(Button button)
        {
            if (activeNavButton != null)
            {
                activeNavButton.BackColor = Color.White;
                activeNavButton.ForeColor = Color.FromArgb(31, 41, 55);
            }

            button.BackColor = Color.FromArgb(72, 128, 255);
            button.ForeColor = Color.White;
            activeNavButton = button;
        }

        private void LoadModule(string moduleName)
        {
            contentPanel.Controls.Clear();

            UserControl moduleControl = moduleName switch
            {
                "Dashboard" => CreateDashboard(),
                "Reports" => new ReportsControl { Dock = DockStyle.Fill },
                "Customers" => new CustomerControl { Dock = DockStyle.Fill },
                "Membership Plans" => new MembershipPlanControl { Dock = DockStyle.Fill },
                "Customer Support" => new CustomerSupportControl { Dock = DockStyle.Fill },
                "Marketing Automation" => new MarketingAutomationControl { Dock = DockStyle.Fill },
                "Sales Force Automation" => new SalesForceControl { Dock = DockStyle.Fill },
                "Staff Management" => new StaffManagementControl { Dock = DockStyle.Fill },
                "Retention" => new RetentionControl { Dock = DockStyle.Fill },
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
                ForeColor = Color.FromArgb(107, 114, 128),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
            };
            contentPanel.Controls.Add(placeholder);
        }

        private DashboardControl CreateDashboard()
        {
            var dashboard = new DashboardControl { Dock = DockStyle.Fill };

            dashboard.NavigateRequested += (targetModule) =>
            {
                if (string.IsNullOrWhiteSpace(targetModule)) return;

                Button? match = null;
                foreach (Control c in sidebarPanel.Controls)
                {
                    if (c is Button b && string.Equals(b.Tag?.ToString(), targetModule, StringComparison.Ordinal))
                    {
                        match = b;
                        break;
                    }
                }

                if (match == null) return;

                SetActiveButton(match);
                LoadModule(targetModule);
            };

            return dashboard;
        }
    }
}