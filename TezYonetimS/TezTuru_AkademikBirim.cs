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
    
    public partial class TezTuru_AkademikBirim
    {
        public int TezTuruId { get; set; }
        public int AkademikBirimId { get; set; }
        public int id { get; set; }
    
        public virtual AkademikBirim AkademikBirim { get; set; }
        public virtual TezTuru TezTuru { get; set; }
    }
}
