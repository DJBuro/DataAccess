//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyAndromedaDataAccessEntityFramework.Model.CustomerDataWarehouse
{
    using System;
    using System.Collections.Generic;
    
    public partial class ContactRecord
    {
        public int Id { get; set; }
        public MyAndromedaDataAccess.Domain.DataWarehouse.ContactType ContactTypeId { get; set; }
        public string Value { get; set; }
        public MyAndromedaDataAccess.Domain.DataWarehouse.MarketingLevelType MarketingLevelId { get; set; }
        public int CustomerId { get; set; }
    
        public virtual CustomerRecord Customer { get; set; }
    }
}
