namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
    public class CheckoutPolicy
    {
        public string PolicyText { get; set; }

        public bool IsCustomerMustAccept { get; set; }

        public bool IsShowInFooter { get; set; }
    }
}