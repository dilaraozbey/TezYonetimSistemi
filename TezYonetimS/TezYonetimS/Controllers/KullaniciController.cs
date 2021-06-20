using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TezYonetimSis.Classlar;
using TezYonetimSis.Models;

namespace TezYonetimSis.Controllers
{
    public class KullaniciController : Controller
    {
        TezYonetimSistemiEntities db = new TezYonetimSistemiEntities();
        [HttpGet]
        public ActionResult UyeGirisi()
        {

            return View();
        }
        [HttpPost]
        public ActionResult UyeGirisi(Kullanici k)
        {
            var uye = db.Kullanici.Where(i => i.Email.Equals(k.Email)).FirstOrDefault();
            if (uye != null)
            {
                if (SifreSifreleme.GetMD5Hash(k.Sifre) == uye.Sifre)
                {

                    if (uye.AktiflikDurumu == 1)
                    {
                        FormsAuthentication.SetAuthCookie(uye.Email, false);
                        if (uye.Rol.RolAdı == "Ogrenci")
                        {
                            return RedirectToAction("TezBasvuru", "Ogrenci");
                        }
                        else if (uye.Rol.RolAdı == "Danisman")
                        {
                            return RedirectToAction("OlusturulmusTezler", "Danisman");
                        }

                    }
                    else if (uye.AktiflikDurumu == 0)
                    {
                        uye.Kod1 = DateTime.Now.Millisecond.ToString();
                        uye.Kod2 = Guid.NewGuid().ToString();
                        db.SaveChanges();
                        MailYollama.SendActivationEmail(uye);
                        ViewBag.Uyari = "Hesabını Aktifleştirmemişsin! Sana tekrar aktivasyon maili yolladık , lütfen maillerini kontrol eder misin?";

                    }
                }
                else
                {
                    ViewBag.Uyari = "Ups!!! Hatalı Giriş Yaptın.";
                }
            }
            else
            {
                ViewBag.Uyari = "Ups!!! Hatalı Giriş Yaptın.";
            }

            return View();
        }


        public ActionResult Cikis()
        {
            FormsAuthentication.SignOut();
            return View("UyeGirisi");
        }



        [HttpGet]
        public ActionResult UyeOl()
        {

            Akademikturlermodel model = new Akademikturlermodel();
            List<AkademikBirim> birimlist = db.AkademikBirim.OrderBy(b => b.BirimAdi).ToList();
            model.BirimlerList = (from s in birimlist select new SelectListItem { Text = s.BirimAdi, Value = s.Id.ToString() }).ToList();
            return View(model);
        }
        [HttpPost]

        public ActionResult UyeOl(Akademikturlermodel k, string sifreDogrulama)
        {
            Akademikturlermodel model = new Akademikturlermodel();
            List<AkademikBirim> birimlist = db.AkademikBirim.OrderBy(b => b.BirimAdi).ToList();
            model.BirimlerList = (from s in birimlist select new SelectListItem { Text = s.BirimAdi, Value = s.Id.ToString() }).ToList();
            Kullanici yeni = new Kullanici();
            ViewBag.Uyari = KullaniciIslemleri.YeniKullanici(k, sifreDogrulama);
            if (ViewBag.Uyari == null)
            {
                if (KullaniciIslemleri.SayiMi(k.YeniKullanici.Email))
                    yeni.RolId = 1;
                else
                    yeni.RolId = 2;

                yeni.Ad = k.YeniKullanici.Ad;
                yeni.Soyad = k.YeniKullanici.Soyad;
                yeni.Email = k.YeniKullanici.Email;
                yeni.TelefonNum = k.YeniKullanici.TelefonNum;
                yeni.Sifre = SifreSifreleme.GetMD5Hash(k.YeniKullanici.Sifre);
                yeni.AkademikBolumId = k.BolumId;
                yeni.AktiflikDurumu = 0;
                yeni.Kod1 = DateTime.Now.Millisecond.ToString();
                yeni.Kod2 = Guid.NewGuid().ToString();
                db.Kullanici.Add(yeni);
                db.SaveChanges();
                MailYollama.SendActivationEmail(yeni);
                ViewBag.Uyari = "Aramıza Hoşgeldin! Mail kutuna aktivasyon linki gönderdik, maillerini kontrol etmeye ne dersin?";
                return View("UyeGirisi");


            }
            return View(model);

        }
        public ActionResult Aktifet()
        {
            if (RouteData.Values["id"] != null)
            {
                string kod = RouteData.Values["id"].ToString();
                Kullanici k = db.Kullanici.Where(x => x.Kod1 + x.Kod2 == kod).FirstOrDefault();
                if (k == null)
                {
                    ViewBag.Uyari = "UPS! Bu işte bir terslik var gibi.";
                }
                else if (k.AktiflikDurumu == 1)
                {
                    ViewBag.Uyari = "Sanki seni daha önce burda görmüştüm?";
                }
                else
                {
                    k.AktiflikDurumu = 1;
                    db.SaveChanges();
                    ViewBag.Uyari = "Aktivasyon başarılı.";
                }

            }


            return View("UyeGirisi");
        }
        [HttpPost]
        public JsonResult ProgramListele(int id)
        {
            List<AkademikProgram> programlist = db.AkademikProgram.Where(p => p.AkademikBirimId == id).OrderBy(p => p.ProgramAdi).ToList();
            List<SelectListItem> itemList = (from i in programlist select new SelectListItem { Value = i.Id.ToString(), Text = i.ProgramAdi }).ToList();
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult BolumListele(int id)
        {
            List<AkademikBolum> bolumlist = db.AkademikBolum.Where(p => p.AkademikProgramId == id).OrderBy(p => p.BolumAdi).ToList();
            List<SelectListItem> itemList = (from i in bolumlist select new SelectListItem { Value = i.Id.ToString(), Text = i.BolumAdi }).ToList();
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DanismanListele(int id)
        {

            List<Kullanici> danismanlist = db.Kullanici.Where(p => p.AkademikBolumId == id).OrderBy(p => p.Ad).ToList();
            List<SelectListItem> itemList = (from i in danismanlist select new SelectListItem { Value = i.Id.ToString(), Text = i.Ad + " " + i.Soyad }).ToList();
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }








    }
}