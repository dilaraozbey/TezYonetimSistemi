using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TezYonetimSis.Classlar;
using TezYonetimSis.Models;

namespace TezYonetimSis.Controllers
{
    [Authorize(Roles = "Ogrenci")]
    public class OgrenciController : Controller
    {
        TezYonetimSistemiEntities db = new TezYonetimSistemiEntities();


        // GET: Ogrenci

        [HttpGet]
        public ActionResult TezBasvuru()
        {
            
            Akademikturlermodel model = new Akademikturlermodel();
            string e = User.Identity.Name.ToString();

            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();


            List<Kullanici> danismanlist = db.Kullanici.Where(a => a.AkademikBolumId == model.YeniKullanici.AkademikBolumId && a.RolId == 2).OrderBy(b => b.Ad).ToList();
            model.DanismanlarList = (from s in danismanlist select new SelectListItem { Text = s.Ad + " " + s.Soyad, Value = s.Id.ToString() }).ToList();

            model.TezlerList = db.Tez.Where(a => a.AkademikBolumId == model.YeniKullanici.AkademikBolumId && a.TezDurumId==1).OrderBy(a => a.OlusturulmaTarihi).ToList();
            List<Kullanici> tumdanismanlar = db.Kullanici.Where(a => a.Rol.RolAdı == "Danisman").OrderBy(b => b.Ad).ToList();
            model.tumdanismanlarlist = (from s in tumdanismanlar select new SelectListItem { Text = s.Ad + " " + s.Soyad, Value = s.Id.ToString() }).ToList();

            return View(model);
        }
        [HttpPost]
        public ActionResult TezBasvuru(string TezAdi, string DanismanId, string aranacakterim)
        {
            

            Akademikturlermodel model = new Akademikturlermodel();
            string e = User.Identity.Name.ToString();

            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();


            List<Kullanici> danismanlist = db.Kullanici.Where(a => a.AkademikBolumId == model.YeniKullanici.AkademikBolumId && a.Rol.RolAdı == "Danisman").OrderBy(b => b.Ad).ToList();
            List<Kullanici> tumdanismanlar = db.Kullanici.Where(a => a.Rol.RolAdı == "Danisman").OrderBy(b => b.Ad).ToList();
            model.tumdanismanlarlist = (from s in tumdanismanlar select new SelectListItem { Text = s.Ad + " " + s.Soyad, Value = s.Id.ToString() }).ToList();

            model.DanismanlarList = (from s in danismanlist select new SelectListItem { Text = s.Ad + " " + s.Soyad, Value = s.Id.ToString() }).ToList();
          
            if (DanismanId == "")
            {

                if (TezAdi == "" && aranacakterim == "")
                {
                    ViewBag.Uyari = "Lütfen kriter belirleyin.";
                    return View(model);

                }
                else
                {
                    model.TezlerList = OgrenciTezSorgulama.TezSorgula(TezAdi, 0, aranacakterim);
                    //int _sayfaNo = SayfaNo ?? 1;
                    //model.TezlerList = (List<Tez>)model.TezlerList.ToPagedList(_sayfaNo, 10);
                    return View(model);
                }
            }
            else
            {
                if (DanismanId != null)
                {
                    int d_Id = Int32.Parse(DanismanId);

                    if (OgrenciTezSorgulama.TezSorgula(TezAdi, d_Id, aranacakterim).Count != 0)
                    {
                        model.TezlerList = OgrenciTezSorgulama.TezSorgula(TezAdi, d_Id, aranacakterim);
                        return View(model);
                    }
                    else
                    {
                        ViewBag.Uyari = "Böyle Bir Sonuç Bulunamadı";

                        return View(model);
                    }
                }
                else return View();
               
            }
        }


        public JsonResult TezListele(string TezAdi, string DanismanAdi, string AnahtarKelime)
        {
            int danismanId = db.Kullanici.Where(x => x.Ad == DanismanAdi).Select(x => x.Id).FirstOrDefault();
            List<Tez> tezlist = db.Tez.Where(p => p.Ad == TezAdi && p.DanismanId == danismanId && p.AnahtarKelimeler == AnahtarKelime).OrderBy(p => p.OlusturulmaTarihi).ToList();
            List<Tez> itemList = (from i in tezlist select new Tez { Id = i.Id, Ad = i.Ad.ToString(), DanismanId = i.DanismanId, AnahtarKelimeler = i.AnahtarKelimeler }).ToList();
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult TezDetay(int? Id)
        {
            Akademikturlermodel model = new Akademikturlermodel();
            string e = User.Identity.Name.ToString();
            if (Id != null)
            {
                model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
                model.YeniTez = db.Tez.Where(x => x.Id == Id).FirstOrDefault();
                model.Danisman = db.Kullanici.Where(x => x.Id == model.YeniTez.DanismanId).FirstOrDefault();
                return View(model);
            }

            return View("TezBasvuru",model);
        

        }
        [HttpPost]
        public ActionResult TezDetay(int id)
        {
            Akademikturlermodel model = new Akademikturlermodel();
            string e = User.Identity.Name.ToString();
            TezBasvuru basvuru = new TezBasvuru();
            Kullanici ogrenci = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
            var sorgu = db.TezBasvuru.Where(x => x.OgrenciId == ogrenci.Id && x.TezId== id).FirstOrDefault();
            if (sorgu == null)
            {
                basvuru.OgrenciId = ogrenci.Id;
                basvuru.BasvuruDurumuId = 1;
                basvuru.TezId = id;
                basvuru.BasvuruTarihi = System.DateTime.Now;
                ViewBag.Basarili = "Başvuru Talebin İletildi.";
                db.TezBasvuru.Add(basvuru);
                db.SaveChanges();
            }
            else
            {
                ViewBag.Uyari = "Ups! Daha önce bu teze başvurmuşsun!";
            }
            model.YeniTez = db.Tez.Where(x => x.Id == id).FirstOrDefault();
            model.YeniKullanici = ogrenci;
            model.Danisman = db.Kullanici.Where(x => x.Id == model.YeniTez.DanismanId).FirstOrDefault();
            return View(model);
        }
        public ActionResult TezOner()
        {

            Akademikturlermodel model = new Akademikturlermodel();
            string e = User.Identity.Name.ToString();

            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();

            List<AkademikBirim> birimlist = db.AkademikBirim.OrderBy(b => b.BirimAdi).ToList();
            model.BirimlerList = (from s in birimlist select new SelectListItem { Text = s.BirimAdi, Value = s.Id.ToString() }).ToList();
            List<Dil> Dillerlist = db.Dil.OrderBy(b => b.Id).ToList();
            model.DillerList = (from s in Dillerlist select new SelectListItem { Text = s.Ad , Value = s.Id.ToString() }).ToList();
            List<Kullanici> danismanlist= db.Kullanici.Where(x=>x.Rol.RolAdı=="Danisman" && x.AkademikBolumId==model.YeniKullanici.AkademikBolumId).ToList();
            model.DanismanlarList = (from s in danismanlist select new SelectListItem { Text = s.Ad + " " + s.Soyad, Value = s.Id.ToString() }).ToList();
            List<TezTuru> tezturlist = db.TezTuru.OrderBy(b => b.Tur).ToList();
            model.TezTurleriList = (from s in tezturlist select new SelectListItem { Text = s.Tur, Value = s.Id.ToString() }).ToList();


            return View(model);
            
        }
        [HttpPost]
        public ActionResult TezOner(Akademikturlermodel t)
        {

            Akademikturlermodel model = new Akademikturlermodel();
            string e = User.Identity.Name.ToString();
            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
            List<AkademikBirim> birimlist = db.AkademikBirim.OrderBy(b => b.BirimAdi).ToList();
            model.BirimlerList = (from s in birimlist select new SelectListItem { Text = s.BirimAdi, Value = s.Id.ToString() }).ToList();
            List<Dil> Dillerlist = db.Dil.OrderBy(b => b.Id).ToList();
            model.DillerList = (from s in Dillerlist select new SelectListItem { Text = s.Ad, Value = s.Id.ToString() }).ToList();
            List<Kullanici> danismanlist = db.Kullanici.Where(x => x.Rol.RolAdı == "Danisman" && x.AkademikBolumId == model.YeniKullanici.AkademikBolumId).ToList();
            model.DanismanlarList = (from s in danismanlist select new SelectListItem { Text = s.Ad+" "+s.Soyad, Value = s.Id.ToString() }).ToList();
            List<TezTuru> tezturlist = db.TezTuru.OrderBy(b => b.Tur).ToList();
            model.TezTurleriList = (from s in tezturlist select new SelectListItem { Text = s.Tur, Value = s.Id.ToString() }).ToList();
            OneriTez yeni = new OneriTez();
           
            ViewBag.Uyari = null;
            var oneritezadivarmi = db.OneriTez.Where(x => x.Ad == t.YeniTez.Ad).FirstOrDefault();
            var tezadivarmi = db.Tez.Where(x => x.Ad == t.YeniTez.Ad).FirstOrDefault();

            if (t.YeniTez.Ad == null || t.YeniTez.Ozet == null ||t.DanismanId==0 ||  t.TezTurId == 0)
            {
                ViewBag.Uyari = "UPS! Eksik kalan bilgiler var.";
            }
            else if(tezadivarmi!=null || oneritezadivarmi != null)
            {
                ViewBag.Uyari = "Bu isimde bir tez mevcut :(";
            }
            else
            {
                yeni.AnahtarKelimeler = t.YeniTez.AnahtarKelimeler;
                yeni.Ozet = t.YeniTez.Ozet;
                yeni.Ad = t.YeniTez.Ad;
                yeni.Ozet = t.YeniTez.Ozet;
                yeni.OgrenciId = model.YeniKullanici.Id;
                yeni.TezTuruId = t.TezTurId;
                yeni.DilId = t.DilId;
                yeni.DanismanId = t.DanismanId;
                yeni.BasvuruDurumuId = 1;
                yeni.TeklifTarihi= System.DateTime.Now.Date;
                db.OneriTez.Add(yeni);
                db.SaveChanges();
                ViewBag.Bilgi = "Tez Önerin Danışmanına İletildi.";
            }


            return View(model);

        }


    }
    
       
}