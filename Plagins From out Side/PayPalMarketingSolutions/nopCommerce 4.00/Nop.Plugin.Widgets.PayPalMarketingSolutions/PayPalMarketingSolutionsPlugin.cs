using System.Collections.Generic;

using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Core;

namespace Nop.Plugin.Widgets.PayPalMarketingSolutions
{
    public class PayPalMarketingSolutionsPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly IWebHelper _webHelper;
        private readonly ISettingService _settingService;

        public PayPalMarketingSolutionsPlugin(IWebHelper webHelper, ISettingService settingService)
        {
            this._webHelper = webHelper;
            this._settingService = settingService;
        }

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string> { "head_html_tag" };
        }

        /// <summary>
        /// Gets a view component for displaying plugin in public store
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <param name="viewComponentName">View component name</param>
        public void GetPublicViewComponent(string widgetZone, out string viewComponentName)
        {
            viewComponentName = "WidgetsPayPalMarketingSolutions";
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsPayPalMarketingSolutions/Configure";
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            // settings
            // TODO: change to production
            
            var settings = new PayPalMarketingSolutionsSettings
            {
                ContainerId = "",
                AdminScriptSrc = "https://www.paypalobjects.com/muse/partners/muse-button-bundle.js",
                FrontendScriptSrc = "https://www.paypal.com/tagmanager/pptm.js",
                PromotionsScript = @"<script>;(function(a,t,o,m,s){a[m]=a[m]||[];a[m].push({t:new Date().getTime(),event:'snippetRun'});var f=t.getElementsByTagName(o)[0],e=t.createElement(o),d=m!=='paypalDDL'?'&m='+m:'';e.async=!0;e.src='{{FRONTEND_JS_SRC}}?id='+s+d;f.parentNode.insertBefore(e,f);})(window,document,'script','paypalDDL','{{CONTAINER_ID}}');</script>"
            };
            _settingService.SaveSetting(settings);

            // locales
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.PayPalMarketingSolutions.ContainerId", "Container Id");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.PayPalMarketingSolutions.PromotionsScript", "PayPal Marketing Solutions frontend area script");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.PayPalMarketingSolutions.FrontendScriptSrc", "PayPal Marketing Solutions frondend area script src");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.PayPalMarketingSolutions.AdminScriptSrc", "PayPal Marketing Solutions admin area script src");

            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            // settings
            _settingService.DeleteSetting<PayPalMarketingSolutionsSettings > ();

            // locales
            this.DeletePluginLocaleResource("Plugins.Widgets.PayPalMarketingSolutions.ContainerId");
            this.DeletePluginLocaleResource("Plugins.Widgets.PayPalMarketingSolutions.PromotionsScript");
            this.DeletePluginLocaleResource("Plugins.Widgets.PayPalMarketingSolutions.FrontendScriptSrc");
            this.DeletePluginLocaleResource("Plugins.Widgets.PayPalMarketingSolutions.AdminScriptSrc");

            base.Uninstall();
        }
    }
}
