namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
    public class CustomerAccount
    {
        public bool IsEnable { get; set; }

        public bool IsEnableAndromedaLogin { get; set; }

        public bool IsEnableFacebookLogin { get; set; }

        public bool IsEnableFacebookAndromeda { get; set; }

        public bool IsEnableOtherFacebook { get; set; }

        public string OtherFacebookAccountID { get; set; }

        public bool IsEnableOrderHistory { get; set; }

        public bool IsEnableRepeatOrder { get; set; }
    }
}