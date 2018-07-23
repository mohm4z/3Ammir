using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.PayPalMarketingSolutions.Models;

using Nop.Core;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Stores;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Services.Security;

namespace Nop.Plugin.Widgets.PayPalMarketingSolutions.Controllers
{
    [Area(AreaNames.Admin)]
    public class WidgetsPayPalMarketingSolutionsController : BasePluginController
    {
        private readonly IWorkContext _workContext;
        private readonly IStoreService _storeService;
        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IPermissionService _permissionService;

        public WidgetsPayPalMarketingSolutionsController(
            IWorkContext workContext,
            IStoreService storeService,
            ILocalizationService localizationService,
            ISettingService settingService,
            IPermissionService permissionService
        ) {
            this._workContext = workContext;
            this._storeService = storeService;
            this._localizationService = localizationService;
            this._settingService = settingService;
            this._permissionService = permissionService;
        }

        [AuthorizeAdmin]
        // [ChildActionOnly]
        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

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
        [AuthorizeAdmin]
        // [ChildActionOnly]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings
            var payPalMarketingSolutionsSettings = _settingService.LoadSetting<PayPalMarketingSolutionsSettings> (0);

            payPalMarketingSolutionsSettings.ContainerId = model.ContainerId;
            _settingService.SaveSetting(payPalMarketingSolutionsSettings);

            //now clear settings cache
            _settingService.ClearCache();

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }
    }
}
