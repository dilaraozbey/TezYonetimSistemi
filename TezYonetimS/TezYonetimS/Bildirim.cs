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
    
    public partial class Bildirim
    {
        public int Id { get; set; }
        public string Aciklama { get; set; }
        public bool Okundu_Okunmadı { get; set; }
        public System.DateTime Tarih { get; set; }
        public Nullable<int> TezId { get; set; }
        public Nullable<int> OneriTezId { get; set; }
        public int DansimanId { get; set; }
        public Nullable<int> Ogrenci1 { get; set; }
        public Nullable<int> Ogrenci2 { get; set; }
        public Nullable<int> YapilacakIsId { get; set; }
        public Nullable<int> AltIsId { get; set; }
        public Nullable<int> SohbetId { get; set; }
    
        public virtual AltIs AltIs { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        public virtual Kullanici Kullanici1 { get; set; }
        public virtual Kullanici Kullanici2 { get; set; }
        public virtual OneriTez OneriTez { get; set; }
        public virtual Sohbet Sohbet { get; set; }
        public virtual Tez Tez { get; set; }
        public virtual YapilacakIs YapilacakIs { get; set; }
    }
}
