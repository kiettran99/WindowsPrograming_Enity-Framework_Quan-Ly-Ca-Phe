//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyCaPhe
{
    using System;
    using System.Collections.Generic;
    
    public partial class ThucAn
    {
        public ThucAn()
        {
            this.ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }
    
        public int IDThucAn { get; set; }
        public string TenThucAn { get; set; }
        public int IDLoaiThucAn { get; set; }
        public Nullable<double> Gia { get; set; }
    
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual LoaiThucAn LoaiThucAn { get; set; }
    }
}
