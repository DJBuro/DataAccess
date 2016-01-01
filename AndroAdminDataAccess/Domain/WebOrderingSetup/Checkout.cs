namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
    public class Checkout
    {
        public bool IsEnableCustomerDetailsCheckoutPage { get; set; }

        public bool IsEnableAddressDetailsCheckoutPage { get; set; }

        public bool IsEnableFutureTimeOrders { get; set; }

        //public CheckoutPolicy CustomPolicy2 { get; set; }
        //public CheckoutPolicy CustomPolicy1 { get; set; }
        //public CheckoutPolicy TermsAndConditions { get; set; }
        
        public void DefaultCheckOutPolicy()
        {
            //TermsAndConditions = new CheckoutPolicy();
            //CustomPolicy1 = new CheckoutPolicy();
            //CustomPolicy2 = new CheckoutPolicy();
            this.IsEnableAddressDetailsCheckoutPage = true;
            this.IsEnableFutureTimeOrders = true;
        }
    }
}