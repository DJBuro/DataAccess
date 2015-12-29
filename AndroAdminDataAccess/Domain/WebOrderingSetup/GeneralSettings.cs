namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
    public class GeneralSettings
    {
        public double MinimumDeliveryAmount { get; set; }

        public double DeliveryCharge { get; set; }

        public bool ApplyDeliveryCharges { get; set; }

        public bool EnableStoreLocatorPage { get; set; }

        public bool IsList { get; set; }

        public bool IsEnterPostCode { get; set; }

        public bool EnableHomePage { get; set; }

        public bool EnableAndromedaLogin { get; set; }

        public bool EnableFacebookLogin { get; set; }

        public string FacebookApplicationId { get; set; }
    }
}