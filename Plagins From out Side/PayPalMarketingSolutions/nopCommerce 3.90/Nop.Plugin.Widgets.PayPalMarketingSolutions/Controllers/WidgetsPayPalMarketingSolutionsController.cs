using System.Web.Mvc;

using Nop.Plugin.Widgets.PayPalMarketingSolutions.Models;

using Nop.Core;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;

namespace Nop.Plugin.Widgets.PayPalMarketingSolutions.Controllers
{
    public class WidgetsPayPalMarketingSolutionsController : BasePluginController
    {

        private readonly IWorkContext _workContext;
        private readonly IStoreService _storeService;
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;

        public WidgetsPayPalMarketingSolutionsController(
            IWorkContext workContext,
            IStoreService storeService,
            ILocalizationService localizationService,
            ISettingService settingService
        ) {
            this._workContext = workContext;
            this._storeService = storeService;
            this._localizationService = localizationService;
            this._settingService = settingService;
        }

        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            //load settings
            var payPalMarketingSolutionsSettings = _settingService.LoadSetting<PayPalMarketingSolutionsSettings>(0);
            var model = new ConfigurationModel
            {
                ContainerId = payPalMarketingSolutionsSettings.ContainerId,
                AdminScriptSrc = payPalMarketingSolutionsSettings.AdminScriptSrc
            };

            return View("~/Plugins/Widgets.PayPalMarketingSolutions/Views/Configure.cshtml", model);
        }

        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure(ConfigurationModel model)
        {
            //load settings
            var payPalMarketingSolutionsSettings = _settingService.LoadSetting<PayPalMarketingSolutionsSettings> (0);

            payPalMarketingSolutionsSettings.ContainerId = model.ContainerId;
            _settingService.SaveSetting(payPalMarketingSolutionsSettings);

            //now clear settings cache
            _settingService.ClearCache();

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }

        public ActionResult PublicInfo(string widgetZone, object additionalData = null)
        {
            var payPalMarketingSolutionsSettings = _settingService.LoadSetting<PayPalMarketingSolutionsSettings> (0);

            // we don't need to invoke the script if there is no container id specified
            if (payPalMarketingSolutionsSettings.ContainerId.Length == 0)
            {
                return Content("");
            }

            var script = payPalMarketingSolutionsSettings.PromotionsScript;
            script = script.Replace("{{CONTAINER_ID}}", payPalMarketingSolutionsSettings.ContainerId);
            script = script.Replace("{{FRONTEND_JS_SRC}}", payPalMarketingSolutionsSettings.FrontendScriptSrc);

            return Content(script);
        }
    }
}
