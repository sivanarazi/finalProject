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
    
    public partial class DealsTBL
    {
        public int Id { get; set; }
        public int Customer { get; set; }
        public int Employee { get; set; }
        public int Details { get; set; }
    
        public virtual CustomersTBL CustomersTBL { get; set; }
        public virtual DealsDetailsTBL DealsDetailsTBL { get; set; }
        public virtual EmployeesTBL EmployeesTBL { get; set; }
    }
}
