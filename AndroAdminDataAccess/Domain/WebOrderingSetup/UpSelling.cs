namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
    public class UpSelling
    {
        public bool IsEnable { get; set; }

        //public List<UpSellingMenuCategory> MenuCategories { get; set; }
        public UpSellingMenuCategory Soup { get; set; }

        public UpSellingMenuCategory Sushi_Boxes { get; set; }

        public UpSellingMenuCategory Sharing_Boxes { get; set; }

        public UpSellingMenuCategory Side_Dishes { get; set; }

        public UpSellingMenuCategory Desserts { get; set; }

        public UpSellingMenuCategory Snacks { get; set; }

        public UpSellingMenuCategory Drinks { get; set; }

        public void DefaultUpSelling()
        {
            this.IsEnable = false;
            //MenuCategories = new List<UpSellingMenuCategory>();
            //List<string> categories = Enum.GetNames(typeof(UpSellingMenuCategoryItem)).ToList();
            //foreach (string category in categories)
            //{
            //    UpSellingMenuCategory tmpCategory = new UpSellingMenuCategory();
            //    tmpCategory.IsEnable = false;
            //    tmpCategory.CategoryName = (UpSellingMenuCategoryItem)Enum.Parse(typeof(UpSellingMenuCategoryItem), category);
            //    tmpCategory.DisplayQuestion = string.Empty;
            //    MenuCategories.Add(tmpCategory);
            //}
        }
    }
}