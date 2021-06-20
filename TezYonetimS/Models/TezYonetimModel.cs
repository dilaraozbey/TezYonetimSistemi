using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TezYonetimS.Models
{
    public class TezYonetimModel
    {
        public Kullanici Ogrenci { get; set; }
        public Kullanici Kullanici { get; set; }
        public Tez Tez { get; set; }
        public List<Tez> TezlerList { get; set; }
        public List<Not> NotlarList { get; set; }
        public List<YapilacakIs> YapılacakIsList { get; set; }
        public List<AltIs> AltIsList { get; set; }
       
    }
}