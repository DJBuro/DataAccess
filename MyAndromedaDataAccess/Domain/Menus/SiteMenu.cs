using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAndromedaDataAccess.Domain.Menus
{
    public enum MenuType 
    {
        Xml,
        Json
    }

    public class SiteMenu
    {
        Guid Id { get; set; }
        Guid SiteId { get; set; }
        public int Version { get; set; }
        public MenuType MenuType { get; set; }
        public string MenuData { get; set; }
    }

}
