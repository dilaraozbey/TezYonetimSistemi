﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TezYonetimSistemiEntities : DbContext
    {
        public TezYonetimSistemiEntities()
            : base("name=TezYonetimSistemiEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AkademikBirim> AkademikBirim { get; set; }
        public virtual DbSet<AkademikBolum> AkademikBolum { get; set; }
        public virtual DbSet<AkademikProgram> AkademikProgram { get; set; }
        public virtual DbSet<AltIs> AltIs { get; set; }
        public virtual DbSet<BasvuruDurumu> BasvuruDurumu { get; set; }
        public virtual DbSet<Bildirim> Bildirim { get; set; }
        public virtual DbSet<Dil> Dil { get; set; }
        public virtual DbSet<IsDurumu> IsDurumu { get; set; }
        public virtual DbSet<IsOncellik> IsOncellik { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<Not> Not { get; set; }
        public virtual DbSet<OneriTez> OneriTez { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Sohbet> Sohbet { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tez> Tez { get; set; }
        public virtual DbSet<TezBasvuru> TezBasvuru { get; set; }
        public virtual DbSet<TezDurumu> TezDurumu { get; set; }
        public virtual DbSet<TezTuru> TezTuru { get; set; }
        public virtual DbSet<TezTuru_AkademikBirim> TezTuru_AkademikBirim { get; set; }
        public virtual DbSet<Toplanti> Toplanti { get; set; }
        public virtual DbSet<Toplanti_YapilacakIs> Toplanti_YapilacakIs { get; set; }
        public virtual DbSet<ToplantiDurumu> ToplantiDurumu { get; set; }
        public virtual DbSet<YapilacakIs> YapilacakIs { get; set; }
        public virtual DbSet<YapilacakIs_Sohbet> YapilacakIs_Sohbet { get; set; }
    }
}
