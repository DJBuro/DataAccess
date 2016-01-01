using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroAdminDataAccess.Domain
{
    public class ThemeAndroWebOrdering
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string FileName { get; set; }
        public string InternalName { get; set; }
        public string Description { get; set; }
        public string ZIPFile { get; set; }
    }
}
