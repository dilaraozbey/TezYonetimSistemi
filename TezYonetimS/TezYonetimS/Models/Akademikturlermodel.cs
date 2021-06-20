using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace TezYonetimSis.Models
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
        public List<SelectListItem> BirimlerList { get; set; }
        public List<SelectListItem> ProgramlarList { get; set; }
        public List<SelectListItem> BolumlerList { get; set; }
        public List<SelectListItem> DanismanlarList { get; set; }
        public List<SelectListItem> tumdanismanlarlist { get; set; }
       
        public List<SelectListItem> DillerList { get; set; }
        public List<SelectListItem> TezTurleriList { get; set; }
        
        public List<Tez> TezlerList { get; set; }
        public Kullanici YeniKullanici { get; set; }
        public Tez YeniTez { get; set; }
        public Kullanici  Danisman { get; set; }
    }

}