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
    
    public partial class Tez
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tez()
        {
            this.Bildirim = new HashSet<Bildirim>();
            this.Not = new HashSet<Not>();
            this.TezBasvuru = new HashSet<TezBasvuru>();
            this.Toplanti = new HashSet<Toplanti>();
            this.YapilacakIs = new HashSet<YapilacakIs>();
        }
    
        public int Id { get; set; }
        public string Ad { get; set; }
        public string TurkceAdi { get; set; }
        public string AnahtarKelimeler { get; set; }
        public string Ozet { get; set; }
        public string PdfLink { get; set; }
        public System.DateTime OlusturulmaTarihi { get; set; }
        public Nullable<System.DateTime> BaslamaTarihi { get; set; }
        public Nullable<System.DateTime> BitisTarihi { get; set; }
        public Nullable<int> SayfaSayisi { get; set; }
        public int AkademikBirimId { get; set; }
        public int AkademikProgramId { get; set; }
        public int AkademikBolumId { get; set; }
        public Nullable<int> OgrenciId1 { get; set; }
        public Nullable<int> OgrenciId2 { get; set; }
        public int DanismanId { get; set; }
        public int TezTuruId { get; set; }
        public int DilId { get; set; }
        public int TezDurumId { get; set; }
    
        public virtual AkademikBirim AkademikBirim { get; set; }
        public virtual AkademikBolum AkademikBolum { get; set; }
        public virtual AkademikProgram AkademikProgram { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bildirim> Bildirim { get; set; }
        public virtual Dil Dil { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        public virtual Kullanici Kullanici1 { get; set; }
        public virtual Kullanici Kullanici2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Not> Not { get; set; }
        public virtual TezDurumu TezDurumu { get; set; }
        public virtual TezTuru TezTuru { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TezBasvuru> TezBasvuru { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Toplanti> Toplanti { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YapilacakIs> YapilacakIs { get; set; }
    }
}
