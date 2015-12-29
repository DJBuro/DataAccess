namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
    public class MenuPageSettings
    {
        //public bool IsEnableCommentsForChef { get; set; }

        //public bool IsEnableWhosItemIsThisFor { get; set; }

        //public bool IsDisplayAlwaysShowToppingsPopupWhenAddingItemsToTheBasket { get; set; }

        //public bool IsEnableImangesForItems { get; set; }

        //public bool IsDisplayItemQuantityDropDown { get; set; }

        //public bool IsDisplayMinimumDeliveryAmountOnMenuPage { get; set; }

        //public bool IsDisplayETDOnMenuPage { get; set; }

        /// <summary>
        /// Gets or sets the is thumbnails enabled.
        /// </summary>
        /// <value>The is thumbnails enabled.</value>
        public bool IsThumbnailsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the is quantity dropdown enabled.
        /// </summary>
        /// <value>The is quantity dropdown enabled.</value>
        public bool IsQuantityDropdownEnabled { get; set; }

        /// <summary>
        /// Gets or sets the is double toppings enabled.
        /// </summary>
        /// <value>The is double toppings enabled.</value>
        public bool IsSingleToppingsOnlyEnabled { get; set; }

        public void DefaultMenuPage()
        {
            this.IsThumbnailsEnabled = true;
            this.IsQuantityDropdownEnabled = true;
            this.IsSingleToppingsOnlyEnabled = false;
        }
    }
}