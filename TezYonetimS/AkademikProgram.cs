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
    
    public partial class AkademikProgram
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AkademikProgram()
        {
            this.AkademikBolum = new HashSet<AkademikBolum>();
            this.Tez = new HashSet<Tez>();
        }
    
        public int Id { get; set; }
        public string ProgramAdi { get; set; }
        public int AkademikBirimId { get; set; }
    
        public virtual AkademikBirim AkademikBirim { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AkademikBolum> AkademikBolum { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tez> Tez { get; set; }
    }
}
