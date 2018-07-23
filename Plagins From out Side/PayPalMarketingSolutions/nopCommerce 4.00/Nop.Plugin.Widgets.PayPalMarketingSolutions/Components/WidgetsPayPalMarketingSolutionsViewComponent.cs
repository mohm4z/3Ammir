using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.PayPalMarketingSolutions.Components
{
    [ViewComponent(Name = "WidgetsPayPalMarketingSolutions")]
    public class WidgetsPayPalMarketingSolutionsViewComponent : NopViewComponent
    {
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;

        public WidgetsPayPalMarketingSolutionsViewComponent(IStoreContext storeContext,
            ISettingService settingService)
        {
            this._storeContext = storeContext;
            this._settingService = settingService;
        }

        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            var payPalMarketingSolutionsSettings = _settingService.LoadSetting<PayPalMarketingSolutionsSettings>(0);

            // we don't need to invoke the script if there is no container id specified
            if (payPalMarketingSolutionsSettings.ContainerId.Length == 0)
            {
                return View("~/Plugins/Widgets.PayPalMarketingSolutions/Views/PublicInfo.cshtml", "");
            }

            var script = payPalMarketingSolutionsSettings.PromotionsScript;
            script = script.Replace("{{CONTAINER_ID}}", payPalMarketingSolutionsSettings.ContainerId);
            script = script.Replace("{{FRONTEND_JS_SRC}}", payPalMarketingSolutionsSettings.FrontendScriptSrc);

            return View("~/Plugins/Widgets.PayPalMarketingSolutions/Views/PublicInfo.cshtml", script);
        }
    }
}
