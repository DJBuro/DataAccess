namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
    public class GeneralSettings
    {
        public decimal MinimumDeliveryAmount { get; set; }

        public decimal DeliveryCharge { get; set; }

        public decimal? OptionalFreeDeliveryThreshold { get; set; }

        public decimal? CardCharge { get; set; } 

        public bool ApplyDeliveryCharges { get; set; }
        
        public bool IsList { get; set; }

        public bool IsEnterPostCode { get; set; }

        public bool EnableStoreLocatorPage { get; set; }

        public bool EnableHomePage { get; set; }

        //these are in CustomerAccountSettings object 
        //public bool EnableAndromedaLogin { get; set; }
        //public bool EnableFacebookLogin { get; set; }
        //public string FacebookApplicationId { get; set; }
    }
}