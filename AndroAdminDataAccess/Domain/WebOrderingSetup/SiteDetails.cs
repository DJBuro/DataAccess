namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
    public class SiteDetails
    {
        public string DomainName { get; set; }

        public string OldDomainName { get; set; }

        public string ParentWebSiteName { get; set; }

        public bool RedirectToParentAfterCheckout { get; set; }

        public bool RedirectToParentOnLogoClick { get; set; }

        public string WebSiteLogo { get; set; }

        public string MobileLogo { get; set; }

        public string WebsiteLogoPath { get; set; }

        public string MobileLogoPath { get; set; }

        public string FaviconPath { set; get; }
    }
}