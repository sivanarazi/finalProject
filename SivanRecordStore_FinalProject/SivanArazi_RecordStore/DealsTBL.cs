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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DealsTBL()
        {
            this.ShippingTBL = new HashSet<ShippingTBL>();
        }
    
        public int Id { get; set; }
        public int Customer { get; set; }
        public int Employee { get; set; }
        public int Cost { get; set; }
        public int Album { get; set; }
    
        public virtual AlbumTBL AlbumTBL { get; set; }
        public virtual CustomerTBL CustomerTBL { get; set; }
        public virtual EmployeesTBL EmployeesTBL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShippingTBL> ShippingTBL { get; set; }
    }
}
