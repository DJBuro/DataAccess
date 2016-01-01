using System;
using System.Linq;

namespace MyAndromedaDataAccess.Domain.Menus.Items
{
    public class ThumbnailImage 
    {
        public Guid Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public string Alt { get; set; }
    }
}
