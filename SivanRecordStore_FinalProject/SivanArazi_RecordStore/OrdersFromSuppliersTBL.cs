//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SivanArazi_RecordStore
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrdersFromSuppliersTBL
    {
        public int Id { get; set; }
        public int Supplier { get; set; }
        public int Employee { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Cost { get; set; }
        public int Album { get; set; }
        public int Amount { get; set; }
    
        public virtual AlbumTBL AlbumTBL { get; set; }
        public virtual EmployeesTBL EmployeesTBL { get; set; }
        public virtual SupplierTBL SupplierTBL { get; set; }
    }
}
