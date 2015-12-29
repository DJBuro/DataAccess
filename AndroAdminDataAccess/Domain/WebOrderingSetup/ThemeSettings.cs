namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
    public class ThemeSettings
    {
        public int Id { get; set; }

        public string ThemePath { get; set; }

        public int? Height { get; set; }

        public int? Width { get; set; }

        public string ThemeName { get; set; }

        public string InternalName { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }
    }
}