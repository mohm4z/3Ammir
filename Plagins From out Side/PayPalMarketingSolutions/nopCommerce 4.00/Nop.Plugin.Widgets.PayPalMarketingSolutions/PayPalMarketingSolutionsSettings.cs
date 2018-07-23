using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.PayPalMarketingSolutions
{
    public class PayPalMarketingSolutionsSettings : ISettings
    {
        public string ContainerId { get; set; }
        public string PromotionsScript { get; set; }
        public string FrontendScriptSrc { get; set; }
        public string AdminScriptSrc { get; set; }
    }
}
