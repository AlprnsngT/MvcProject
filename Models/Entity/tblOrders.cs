//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _50DersteMvcProjeKampi.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblOrders
    {
        public short OrderId { get; set; }
        public Nullable<short> ProductId { get; set; }
        public Nullable<short> CustomerId { get; set; }
        public Nullable<byte> OrderStocks { get; set; }
        public Nullable<decimal> OrderPrice { get; set; }
    
        public virtual tblCustomers tblCustomers { get; set; }
        public virtual tblProducts tblProducts { get; set; }
    }
}
