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
    
    public partial class Not
    {
        public int Id { get; set; }
        public string Aciklama { get; set; }
        public Nullable<int> Ogrenci1Id { get; set; }
        public Nullable<int> Ogrenci2Id { get; set; }
        public Nullable<int> DanismanId { get; set; }
        public Nullable<int> YapılacakisId { get; set; }
        public Nullable<int> TezId { get; set; }
        public Nullable<int> ToplantiId { get; set; }
    
        public virtual Kullanici Kullanici { get; set; }
        public virtual Kullanici Kullanici1 { get; set; }
        public virtual Kullanici Kullanici2 { get; set; }
        public virtual Tez Tez { get; set; }
        public virtual Toplanti Toplanti { get; set; }
        public virtual YapilacakIs YapilacakIs { get; set; }
    }
}
