//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataWarehouseDataAccessEntityFramework.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        public Customer()
        {
            this.Contacts = new HashSet<Contact>();
            this.CustomerAddresses = new HashSet<CustomerAddress>();
            this.OrderHeaders = new HashSet<OrderHeader>();
            this.PasswordResetRequests = new HashSet<PasswordResetRequest>();
        }
    
        public System.Guid ID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> AddressId { get; set; }
        public int ACSAplicationId { get; set; }
        public System.DateTime RegisteredDateTime { get; set; }
        public Nullable<System.Guid> CustomerAccountId { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual CustomerAccount CustomerAccount { get; set; }
        public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; }
        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }
        public virtual ICollection<PasswordResetRequest> PasswordResetRequests { get; set; }
    }
}
