using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TezYonetimSis.Models
{
    public class TezOlusturModel
    {
        public TezOlusturModel()
        {
            this.ProgramlarList = new List<SelectListItem>();
            ProgramlarList.Add(new SelectListItem { Text = "Seçiniz", Value = "" });
            this.BolumlerList = new List<SelectListItem>();
            BolumlerList.Add(new SelectListItem { Text = " Seçiniz", Value = "" });
            this.TezTurleriList = new List<SelectListItem>();
            TezTurleriList.Add(new SelectListItem { Text = "Seçiniz", Value = "" });
           
            
        }
        public int BirimId { get; set; }
        public int ProgramId { get; set; }
        public int BolumId { get; set; }
        public int Tez_Id { get; set; }
        public string Yazar1 { get; set; }
        public string Yazar2 { get; set; }
        public int DilId { get; set; }
        public int TezTurId { get; set; }
        public String TurId { get; set; }
        public String DurumId { get; set; }
        public String Bolum { get; set; }
        public List<SelectListItem> BirimlerList { get; set; }
        public List<SelectListItem> ProgramlarList { get; set; }
        public List<SelectListItem> BolumlerList { get; set; }
        public List<SelectListItem> DillerList { get; set; }
        public List<SelectListItem> TezTurleriList { get; set; }
        public List<SelectListItem> TezlerList { get; set; }
        public List<TezBasvuru> TezTalepleri { get; set; }
        public List<Tez> SecilenTezlerList { get; set; }
        public List<SelectListItem> TezDurumlarıList { get; set; }
        public List<OneriTez> TezOnerileriList { get; set; }
        public List<Not> NotlarList { get; set; }

        public Tez YeniTez { get; set; }
        public Kullanici YeniKullanici { get; set; }
        public OneriTez oneritez { get; set; }
        public Kullanici ogrenci { get; set; }

       public string YeniNot { get; set; }
     


    }

}
