using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace TezYonetimS.Models
{
    public class Akademikturlermodel
    {
        public Akademikturlermodel()
        {
            this.ProgramlarList = new List<SelectListItem>();
            ProgramlarList.Add(new SelectListItem { Text = "Seçiniz", Value = "" });
            this.BolumlerList = new List<SelectListItem>();
            BolumlerList.Add(new SelectListItem { Text = " Seçiniz", Value = "" });
            this.DanismanlarList = new List<SelectListItem>();
            DanismanlarList.Add(new SelectListItem { Text = "Seçiniz", Value = "" });
            this.TezTurleriList = new List<SelectListItem>();
            TezTurleriList.Add(new SelectListItem { Text = "Seçiniz", Value = "" });
        }
        public string aranacakalan { get; set; }
        public string aranacakterim { get; set; }
        public int BirimId { get; set; }
        public int ProgramId { get; set; }
        public int BolumId { get; set; }
        public String DanismanAdi { get; set; }
        public int DanismanId { get; set; }
        public int TezTurId { get; set; }
        public int DilId { get; set; }
        public int SayfaNo { get; set; }
        public string TezAdi { get; set; }
        public int IsOncelikId { get; set; }
        public int IsDurumId { get; set; }
        public int Is_Id { get; set; }
        public string YeniNot { get; set; }
        public int YapilacakIs_Id { get; set; }
        public List<SelectListItem> BirimlerList { get; set; }
        public List<SelectListItem> ProgramlarList { get; set; }
        public List<SelectListItem> BolumlerList { get; set; }
        public List<SelectListItem> DanismanlarList { get; set; }
        public List<SelectListItem> tumdanismanlarlist { get; set; }

        public List<SelectListItem> DillerList { get; set; }
        public List<SelectListItem> TezTurleriList { get; set; }
        public List<SelectListItem> TezDurumlarıList { get; set; }
        public List<Tez> SecilenTezlerList { get; set; }
        public List<Not> NotlarList { get; set; }
        public List<Toplanti> TezToplantilarList { get; set; }
        public List<Tez> TezlerList { get; set; }
        public List<Toplanti> TezOnelerilerList { get; set; }
        public List<SelectListItem> IsDurumList { get; set; }
        public List<SelectListItem> IsOncelikList { get; set; }
        public List<YapilacakIs> yapilacakIsList { get; set; }
        public List<AltIs> AltIsList { get; set; }
        public List<YapilacakIs> bagliIslerList { get; set; }
        public List<YapilacakIs> bagliOlacagiIslerList { get; set; }
        public List<SelectListItem> YapilacakIslerSelectedList { get; set; }
        public Kullanici YeniKullanici { get; set; }
        public Tez YeniTez { get; set; }
        public Kullanici Danisman { get; set; }
        public YapilacakIs Is { get; set; }

    }

}