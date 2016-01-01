using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyAndromeda.Web.Areas.Reporting.ViewModels
{
    public class TopSalesViewModel
    {
        public TopSalesItem[] TopItems { get; set; }
    }

    public class TopSalesItem 
    {
        public int Position { get; set; }
        public string Name { get; set; }
        public decimal Total { get; set; }
    }
}