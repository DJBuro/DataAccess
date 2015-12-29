using System.Collections.Generic;

namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
    /// <summary>
    /// TBD Carousel
    /// </summary>
    public class Carousel
    {
        public string Name { get; set; }

        public CarouselContainerOptions CarouselContainer { get; set; }

        public CarouselNavigatorOptions CarouselNavigator { get; set; }

        public List<CarouselItem> Items { get; set; }

        //public List<CarouselMenuItem> MenuItems { get; set; }
        //public List<CarouselHTMLItem> HTMLItems { get; set; }
        //public List<CarouselDealItem> DealItems { get; set; }
        //public List<CarouselImageItem> ImageItems { get; set; }
        public Carousel DefaultCarousel()
        {
            this.Name = "Featured";
            this.CarouselContainer = new CarouselContainerOptions();
            this.CarouselNavigator = new CarouselNavigatorOptions();
            this.Items = new List<CarouselItem>();

            return this;
        }
    }
}