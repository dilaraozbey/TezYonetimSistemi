//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TezYonetimS
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sohbet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sohbet()
        {
            this.Bildirim = new HashSet<Bildirim>();
            this.YapilacakIs_Sohbet = new HashSet<YapilacakIs_Sohbet>();
        }
    
        public int Id { get; set; }
        public Nullable<int> OgrenciId { get; set; }
        public Nullable<int> DanismanId { get; set; }
        public string Mesaj { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bildirim> Bildirim { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        public virtual Kullanici Kullanici1 { get; set; }
        public virtual Kullanici Kullanici2 { get; set; }
        public virtual Kullanici Kullanici3 { get; set; }
        public virtual Kullanici Kullanici4 { get; set; }
        public virtual Kullanici Kullanici5 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YapilacakIs_Sohbet> YapilacakIs_Sohbet { get; set; }
    }
}
