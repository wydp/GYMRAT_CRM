using System.Windows.Forms;

namespace CRM.winforms.Controls
{
    public partial class MarketingAutomationControl : UserControl
    {
        public MarketingAutomationControl()
        {
            InitializeComponent();

            var campaignControl = new CampaignControl { Dock = DockStyle.Fill };
            tabCampaigns.Controls.Add(campaignControl);

            var promotionControl = new PromotionControl { Dock = DockStyle.Fill };
            tabPromotions.Controls.Add(promotionControl);

            var promoCodeControl = new PromoCodeControl { Dock = DockStyle.Fill };
            tabPromoCodes.Controls.Add(promoCodeControl);
        }
    }
}