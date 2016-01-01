namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
    public class MenuPage
    {
        public bool IsEnableCommentsForChef { get; set; }

        public bool IsEnableWhosItemIsThisFor { get; set; }

        public bool IsDisplayAlwaysShowToppingsPopupWhenAddingItemsToTheBasket { get; set; }

        public bool IsEnableImangesForItems { get; set; }

        public bool IsDisplayItemQuantityDropDown { get; set; }

        public bool IsEnableDoubleToppings { get; set; }

        public bool IsDisplayMinimumDeliveryAmountOnMenuPage { get; set; }

        public bool IsDisplayETDOnMenuPage { get; set; }

        public void DefaultMenuPage()
        {
            this.IsEnableImangesForItems = true;
            this.IsDisplayItemQuantityDropDown = true;
            this.IsEnableDoubleToppings = true;
        }
    }
}