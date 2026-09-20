using System.Windows.Forms;

namespace CRM.winforms.Controls
{
    public partial class CustomerSupportControl : UserControl
    {
        public CustomerSupportControl()
        {
            InitializeComponent();

            var inquiryControl = new InquiryControl { Dock = DockStyle.Fill };
            tabInquiries.Controls.Add(inquiryControl);

            var feedbackControl = new FeedbackControl { Dock = DockStyle.Fill };
            tabFeedback.Controls.Add(feedbackControl);
        }
    }
}
