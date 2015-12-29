using System;
using System.Linq;
using Newtonsoft.Json;

namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
    /// <summary>
    /// TBD: Class to be moved to another layer and method access-specifiers to be modified.
    /// TBD: JSON serialized-object format to be freezed.
    /// </summary>
    public class WebSiteConfigurations
    {
        public WebSiteConfigurations()
        {
            this.Home = new AndroAdminDataAccess.Domain.WebOrderingSetup.WebsiteHome();
            this.Home.DefaultWebSiteHome(); // Don't replace with constructor (deserialize with Json.NET will result in duplicates)

            this.LegalNotices = new LegalNotices();

            this.SiteDetails = new SiteDetails();
            this.ThemeSettings = new ThemeSettings();
            this.GeneralSettings = new GeneralSettings();
            this.HomePageSettings = new HomePageSettings();
            this.SocialNetworkSettings = new SocialNetwork();
            this.SocialNetworkSettings.DefaultSocialNetwork();

            this.CustomerAccountSettings = new CustomerAccount();            

            this.MenuPageSettings = new MenuPage();
            this.MenuPageSettings.DefaultMenuPage();

            this.CheckoutSettings = new Checkout();
            this.CheckoutSettings.DefaultCheckOutPolicy();

            this.UpSellingSettings = new UpSelling();

            this.LastUpdatedUtc = DateTime.UtcNow;
            this.AnalyticsSettings = new Analytics();
            
            this.CustomThemeSettings = new CustomThemeSettings();
            this.CustomThemeSettings.DefaultCustomThemeSettings();

        }

        public int WebSiteId { get; set; }

        public string WebSiteName { get; set; }

        public string ACSApplicationId { get; set; }

        public string LiveDomainName { get; set; }

        public string PreviewDomainName { get; set; }

        public string OldLiveDomainName { get; set; }

        public string OldPreviewDomainName { get; set; }

        public bool IsWebSiteEnabled { get; set; }

        public int? ChainId { get; set; }

        public int SubScriptionTypeId { get; set; }

        public string DisabledReason { get; set; }

        public AndroAdminDataAccess.Domain.WebOrderingSetup.WebsiteHome Home { get; set; }

        public LegalNotices LegalNotices { get; set; }

        public SiteDetails SiteDetails { get; set; }

        public GeneralSettings GeneralSettings { get; set; }

        public ThemeSettings ThemeSettings { get; set; }

        public HomePageSettings HomePageSettings { get; set; }

        public SocialNetwork SocialNetworkSettings { get; set; }

        public CustomerAccount CustomerAccountSettings { get; set; }

        public MenuPage MenuPageSettings { get; set; }

        public Checkout CheckoutSettings { get; set; }

        public UpSelling UpSellingSettings { get; set; }

        public DateTime LastUpdatedUtc { get; set; }

        public Analytics AnalyticsSettings { set; get; }

        public CustomThemeSettings CustomThemeSettings { set; get; }

        public static string SerializeJson(Object obj)
        {
            if (obj == null)
                return string.Empty;

            var result = JsonConvert.SerializeObject(obj);
            return result;
        }

        public static WebSiteConfigurations DeserializeJson(string obj)
        {
            if (String.IsNullOrEmpty(obj.Trim()))
                return new WebSiteConfigurations();

            var result = JsonConvert.DeserializeObject<WebSiteConfigurations>(obj);
            return result;
        }
    }
}
