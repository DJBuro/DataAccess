namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
    public class CarouselMenuItem
    {
        public bool IsEnabled { get; set; }

        public bool IsOverlayText { get; set; }

        public bool IsOverrideDefaultMenuImage { get; set; }// applicable for Menu type

        public string ImageUrl { get; set; }
    }
}