using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TezYonetimS.Models
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

        public string YeniNot { get; set; }
        public int Id { get; set; }
        public int Cesit { get; set; }
        public int BirimId { get; set; }
        public int ProgramId { get; set; }
        public int BolumId { get; set; }
        public int Tez_Id { get; set; }
        public int YapilacakIs_Id { get; set; }
        public int Is_Id { get; set; }
        public string Yazar1 { get; set; }
        public string Yazar2 { get; set; }
        public int DilId { get; set; }
        public int TezTurId { get; set; }
        public int IsOncelikId { get; set; }
        public int IsDurumId { get; set; }
        public string TurId { get; set; }
        public string DurumId { get; set; }
        public string Bolum { get; set; }
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
        public List<Toplanti> TezToplantilarList { get; set; }
        public List<Toplanti> TezOnelerilerList { get; set; }
        public List<SelectListItem> IsDurumList { get; set; }
        public List<SelectListItem> IsOncelikList { get; set; }
        public List<YapilacakIs> yapilacakIsList { get; set; }
        public Tez YeniTez { get; set; }
        public Kullanici YeniKullanici { get; set; }
        public OneriTez oneritez { get; set; }
        public Kullanici ogrenci { get; set; }
        public YapilacakIs Is { get; set; }
        public Toplanti YeniToplanti { get; set; }
        public List<AltIs> AltIsList { get; set; }
        public List<YapilacakIs> bagliIslerList { get; set; }
        public List<YapilacakIs> bagliOlacagiIslerList { get; set; }
        public List<SelectListItem> YapilacakIslerSelectedList { get; set; }



    }

}
