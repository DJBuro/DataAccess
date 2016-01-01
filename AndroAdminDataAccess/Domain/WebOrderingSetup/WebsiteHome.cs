namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
    public class WebsiteHome
    {
        public Title Welcome { get; set; }

        public Title Menu { get; set; }

        public Carousel[] Carousels { get; set; }

        //public Carousel Carousel1 { get; set; }

        //public Carousel Carousel2 { get; set; }
        public void DefaultWebSiteHome()
        {
            this.Welcome = new Title();
            this.Menu = new Title();

            this.Carousels = new[]
            {
                new Carousel().DefaultCarousel()
            };
            //Carousel1 = ;
            //Carousel1.DefaultCarousel();
            //Carousel2 = new Carousel();
            //Carousel2.DefaultCarousel();
        }
    }
}