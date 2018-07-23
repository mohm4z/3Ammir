using System.Web.Mvc;

using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widgets.PayPalMarketingSolutions.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.PayPalMarketingSolutions.ContainerId")]
        public string ContainerId { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.GoogleAnalytics.PromotionsScript")]
        [AllowHtml]
        public string PromotionsScript { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.GoogleAnalytics.FrontendScriptSrc")]
        [AllowHtml]
        public string FrontendScriptSrc { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.GoogleAnalytics.AdminScriptSrc")]
        [AllowHtml]
        public string AdminScriptSrc { get; set; }
    }
}
