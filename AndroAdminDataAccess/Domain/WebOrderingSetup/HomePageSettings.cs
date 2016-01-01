namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
    public class HomePageSettings
    {
        public bool EnableStoreDetails { get; set; }

        public bool ShowMinimumDelivery { get; set; }

        public bool ShowDeliveryCharge { get; set; }

        public bool ShowETD { get; set; }

        public bool PostcodeLocator { set; get; }
    }
}