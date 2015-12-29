using System;

namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
    public class CarouselItem
    {
        public Guid Id { get; set; }

        public bool IsOverlayText { get; set; }

        // applicable only for Menu type
        public bool IsOverrideDefaultMenuImage { get; set; }

        // applicable only for HTML type
        public string HTML { get; set; }
        
        public string ImageUrl { get; set; }

        public string Description { set; get; }

        public string Type { get; set; }
    }
}