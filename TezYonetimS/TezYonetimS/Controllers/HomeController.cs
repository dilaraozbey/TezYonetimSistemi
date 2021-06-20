using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TezYonetimSis.Classlar;
using TezYonetimSis.Models;

namespace TezYonetimSis.Controllers
{
    [Authorize(Roles = "Danisman")]
    public class DanismanController : Controller
    {
         TezYonetimSistemiEntities db = new TezYonetimSistemiEntities();



        [HttpGet]
        public ActionResult TezOlustur()
        {
            TezOlusturModel model = new TezOlusturModel();
            string e = User.Identity.Name.ToString();

            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();

            List<AkademikBirim> birimlist = db.AkademikBirim.OrderBy(b => b.BirimAdi).ToList();
            model.BirimlerList = (from s in birimlist select new SelectListItem { Text = s.BirimAdi, Value = s.Id.ToString() }).ToList();
            List<Dil> Dillerlist = db.Dil.OrderBy(b => b.Id).ToList();
            model.DillerList = (from s in Dillerlist select new SelectListItem { Text = s.Ad, Value = s.Id.ToString() }).ToList();

            return View(model);

        }
        [HttpPost]
        public ActionResult TezOlustur(TezOlusturModel t, string yazar1, string yazar2)
        {

            TezOlusturModel model = new TezOlusturModel();
            string e = User.Identity.Name.ToString();
            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
            List<AkademikBirim> birimlist = db.AkademikBirim.OrderBy(b => b.BirimAdi).ToList();
            model.BirimlerList = (from s in birimlist select new SelectListItem { Text = s.BirimAdi, Value = s.Id.ToString() }).ToList();
            List<Dil> Dillerlist = db.Dil.OrderBy(b => b.Id).ToList();
            model.DillerList = (from s in Dillerlist select new SelectListItem { Text = s.Ad, Value = s.Id.ToString() }).ToList();
            Tez yeni = new Tez();
            Kullanici k = new Kullanici();
            ViewBag.Uyari = null;

            if (t.YeniTez.Ad == null || t.YeniTez.Ozet == null || t.BirimId == 0 || t.BolumId == 0 || t.ProgramId == 0 || t.DilId == 0 || t.TezTurId == 0)
            {
                ViewBag.Uyari = "UPS! Eksik kalan bilgiler var.";
            }
            else
            {
                yeni.Ad = t.YeniTez.Ad;
                yeni.Ozet = t.YeniTez.Ozet;
                yeni.AkademikBirimId = t.BirimId;
                yeni.AkademikBolumId = t.BolumId;
                yeni.AkademikProgramId = t.ProgramId;
                yeni.TezTuruId = t.TezTurId;
                yeni.DilId = t.DilId;
                if (t.YeniTez.TurkceAdi == "" || t.YeniTez.TurkceAdi == null)
                {
                    if (KullaniciIslemleri.TurkceMi(t) != null)
                    {
                        yeni.TurkceAdi = KullaniciIslemleri.TurkceMi(t);
                    }
                }
                else if (t.YeniTez.TurkceAdi != null)
                {
                    yeni.TurkceAdi = t.YeniTez.TurkceAdi;
                }
                ViewBag.Uyari = KullaniciIslemleri.YeniTezOlustur(t);
                if (ViewBag.Uyari == null)
                {
                    if (yazar1 != "" && yazar2 == "")
                    {
                        ViewBag.Uyari = KullaniciIslemleri.Sorgula(yazar1);
                        if (ViewBag.Uyari == null)
                        {
                            var yazar = db.Kullanici.Where(i => (i.Ad + " " + i.Soyad).Equals(yazar1) || i.Email.Equals(yazar1)).FirstOrDefault();
                            yeni.OgrenciId1 = yazar.Id;
                        }
                    }
                    else if (yazar1 == "" && yazar2 != "")
                    {
                        ViewBag.Uyari = KullaniciIslemleri.Sorgula(yazar2);
                        if (ViewBag.Uyari == null)
                        {
                            var yazar = db.Kullanici.Where(i => (i.Ad + " " + i.Soyad).Equals(yazar2) || i.Email.Equals(yazar2)).FirstOrDefault();
                            yeni.OgrenciId1 = yazar.Id;
                        }
                    }
                    else if (yazar1 != "" && yazar2 != "")
                    {
                        ViewBag.Uyari = KullaniciIslemleri.Sorgula2(yazar1, yazar2);
                        if (ViewBag.Uyari == null)
                        {
                            var y1 = db.Kullanici.Where(i => (i.Ad + " " + i.Soyad).Equals(yazar1) || i.Email.Equals(yazar1)).FirstOrDefault();
                            var y2 = db.Kullanici.Where(i => (i.Ad + " " + i.Soyad).Equals(yazar2) || i.Email.Equals(yazar2)).FirstOrDefault();
                            yeni.OgrenciId1 = y1.Id;
                            yeni.OgrenciId2 = y1.Id;
                        }
                    }
                }
            }
            if (ViewBag.Uyari == null)
            {
                if (t.YeniTez.AnahtarKelimeler != null)
                {
                    yeni.AnahtarKelimeler = t.YeniTez.AnahtarKelimeler;

                }
                if (yazar1 != "" || yazar2 != "")
                {
                    yeni.TezDurumId = 2;
                }
                else
                {
                    yeni.TezDurumId = 1;
                }


                yeni.OlusturulmaTarihi = System.DateTime.Today;
                Kullanici d = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
                yeni.DanismanId = d.Id;
                db.Tez.Add(yeni);
                db.SaveChanges();
                ViewBag.Bilgi = "Tez Başarıyla Oluşturuldu";

            }


            return View(model);

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
        public JsonResult TezListele(int id)
        {
            var listtur = (from b in db.AkademikBirim
                           join a in db.TezTuru_AkademikBirim on b.Id equals a.AkademikBirimId
                           join t in db.TezTuru on a.TezTuruId equals t.Id
                           where (id == b.Id)
                           select t);
            List<TezTuru> tezturulist = listtur.OrderBy(p => p.Tur).ToList();
            List<SelectListItem> itemList = (from i in tezturulist select new SelectListItem { Value = i.Id.ToString(), Text = i.Tur }).ToList();
            return Json(itemList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult TezTalepleri()
        {

            TezOlusturModel model = new TezOlusturModel();
            string e = User.Identity.Name.ToString();
            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();


            List<Tez> tezlist = db.Tez.Where(a => a.DanismanId == model.YeniKullanici.Id && a.TezDurumId == 1).OrderBy(b => b.Ad).ToList();
            model.TezlerList = (from s in tezlist select new SelectListItem { Text = s.Ad, Value = s.Id.ToString() }).ToList();


            List<TezTuru> tezturu = db.TezTuru.OrderBy(b => b.Tur).ToList();
            model.TezTurleriList = (from s in tezturu
                                    select new SelectListItem
                                    {
                                        Text = s.Tur,
                                        Value = s.Id.ToString()
                                    }).ToList();
            model.TezTalepleri = (from t in db.Tez
                                  join basvuru in db.TezBasvuru on t.Id equals basvuru.TezId
                                  where t.DanismanId == model.YeniKullanici.Id && t.TezDurumu.Durum == "Açık" && basvuru.BasvuruDurumu.Durum == "Beklemede"
                                  select basvuru).ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult TezTalepleri(string Tez_Id, string TezTurId)
        {
            TezOlusturModel model = new TezOlusturModel();
            string e = User.Identity.Name.ToString();
            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();

            List<Tez> tezlist = db.Tez.Where(a => a.DanismanId == model.YeniKullanici.Id && a.TezDurumId == 1).OrderBy(b => b.Ad).ToList();
            model.TezlerList = (from s in tezlist select new SelectListItem { Text = s.Ad, Value = s.Id.ToString() }).ToList();


            List<TezTuru> tezturu = db.TezTuru.OrderBy(b => b.Tur).ToList();
            model.TezTurleriList = (from s in tezturu
                                    select new SelectListItem
                                    {
                                        Text = s.Tur,
                                        Value = s.Id.ToString()
                                    }).ToList();
            if (Tez_Id != "" && TezTurId != "")
            {
                int tez_Id = Int32.Parse(Tez_Id);
                int tezturu_Id = Int32.Parse(TezTurId);
                model.TezTalepleri = TezSorgulama.TalepSorgu(tez_Id, tezturu_Id, model.YeniKullanici.Id);

            }
            else if (Tez_Id == "" && TezTurId != "")
            {

                int tezturu_Id = Int32.Parse(TezTurId);
                model.TezTalepleri = TezSorgulama.TalepSorgu(0, tezturu_Id, model.YeniKullanici.Id);

            }
            else if (Tez_Id != "" && TezTurId == "")
            {
                int tez_Id = Int32.Parse(Tez_Id);

                model.TezTalepleri = TezSorgulama.TalepSorgu(tez_Id, 0, model.YeniKullanici.Id);

            }
            else
            {
                ViewBag.Uyari = "Lütfen Kriter Belirleyiniz!";
                return View(model);
            }
            if (model.TezTalepleri.Count == 0)
            {
                ViewBag.Uyari = "Bu Özelliklerde Talep Bulunamadı.";
            }


            return View(model);
        }

        [HttpGet]
        public ActionResult TezOnerileri()
        {
            TezOlusturModel model = new TezOlusturModel();
            string e = User.Identity.Name.ToString();
            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
            List<TezTuru> turlist = db.TezTuru.OrderBy(b => b.Tur).ToList();
            model.TezTurleriList = (from s in turlist select new SelectListItem { Text = s.Tur, Value = s.Id.ToString() }).ToList();

            List<AkademikBolum> bolumler = db.AkademikBolum.Where(a => a.Id == model.YeniKullanici.AkademikBolumId).OrderBy(b => b.BolumAdi).ToList();
            model.BolumlerList = (from s in bolumler select new SelectListItem { Text = s.BolumAdi, Value = s.Id.ToString() }).ToList();
            model.TezOnerileriList = db.OneriTez.Where(x => x.DanismanId == model.YeniKullanici.Id && x.BasvuruDurumuId == 1).ToList();


            return View(model);
        }
        [HttpPost]
        public ActionResult TezOnerileri(TezOlusturModel t)
        {
            TezOlusturModel model = new TezOlusturModel();
            string e = User.Identity.Name.ToString();
            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
            t.YeniKullanici = model.YeniKullanici;
            List<TezTuru> turlist = db.TezTuru.OrderBy(b => b.Tur).ToList();
            model.TezTurleriList = (from s in turlist select new SelectListItem { Text = s.Tur, Value = s.Id.ToString() }).ToList();
            List<AkademikBolum> bolumler = db.AkademikBolum.Where(a => a.Id == model.YeniKullanici.AkademikBolumId).OrderBy(b => b.BolumAdi).ToList();
            model.BolumlerList = (from s in bolumler select new SelectListItem { Text = s.BolumAdi, Value = s.Id.ToString() }).ToList();
            if (t.Bolum == null && t.TurId == null)
            {
                ViewBag.Uyari = "Lütfen Kriter Seçiniz.";
            }
            else
            {
                if (TezSorgulama.OneriTezSorgula(t).Count != 0)
                {
                    model.TezOnerileriList = TezSorgulama.OneriTezSorgula(t);
                }
                else
                {
                    ViewBag.Uyari = "İstenilen Kriterde Öneri Bulunamadı.";
                }
            }
            return View(model);
        }
        public ActionResult OneriTezDetay(int Id)
        {
            TezOlusturModel model = new TezOlusturModel();
            string e = User.Identity.Name.ToString();

            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
            model.oneritez = db.OneriTez.Where(x => x.Id == Id).FirstOrDefault();
            model.ogrenci = db.Kullanici.Where(x => x.Id == model.oneritez.OgrenciId).FirstOrDefault();
            return View(model);

        }

        [HttpGet]
        public ActionResult OlusturulmusTezler()
        {

            TezOlusturModel model = new TezOlusturModel();
            string e = User.Identity.Name.ToString();
            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
            List<TezTuru> turlist = db.TezTuru.OrderBy(b => b.Tur).ToList();
            model.TezTurleriList = (from s in turlist select new SelectListItem { Text = s.Tur, Value = s.Id.ToString() }).ToList();

            List<AkademikBolum> bolumler = db.AkademikBolum.Where(a => a.Id == model.YeniKullanici.AkademikBolumId).OrderBy(b => b.BolumAdi).ToList();
            model.BolumlerList = (from s in bolumler select new SelectListItem { Text = s.BolumAdi, Value = s.Id.ToString() }).ToList();

            List<TezDurumu> durumlar = (from tez in db.Tez join durum in db.TezDurumu on tez.TezDurumId equals durum.Id where tez.DanismanId == model.YeniKullanici.Id select durum).Distinct().ToList();
            model.TezDurumlarıList = (from s in durumlar select new SelectListItem { Text = s.Durum, Value = s.Id.ToString() }).ToList();
            model.SecilenTezlerList = db.Tez.Where(x => x.DanismanId == model.YeniKullanici.Id && (x.TezDurumu.Durum == "Açık" || x.TezDurumu.Durum == "Kapalı")).OrderByDescending(x => x.Id).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult OlusturulmusTezler(TezOlusturModel t)
        {
            TezOlusturModel model = new TezOlusturModel();
            string e = User.Identity.Name.ToString();
            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
            List<TezTuru> turlist = db.TezTuru.OrderBy(b => b.Tur).ToList();
            model.TezTurleriList = (from s in turlist select new SelectListItem { Text = s.Tur, Value = s.Id.ToString() }).ToList();

            List<AkademikBolum> bolumler = db.AkademikBolum.Where(a => a.Id == model.YeniKullanici.AkademikBolumId).OrderBy(b => b.BolumAdi).ToList();
            model.BolumlerList = (from s in bolumler select new SelectListItem { Text = s.BolumAdi, Value = s.Id.ToString() }).ToList();
            t.YeniKullanici = model.YeniKullanici;
            if (t.Bolum == null && t.TurId == null)
            {
                ViewBag.Uyari = "Lütfen Kriter Seçiniz.";
            }
            else
            {
                if (TezSorgulama.OlusturulmusTezSorgulama(t).Count != 0)
                {
                    model.SecilenTezlerList = TezSorgulama.OlusturulmusTezSorgulama(t);
                }
                else
                {
                    ViewBag.Uyari = "İstenilen Kriterde Tez Bulunamadı.";
                }
            }


            return View(model);
        }
        [HttpGet]
        public ActionResult DevamEdenTezler()
        {
            TezOlusturModel model = new TezOlusturModel();
            string e = User.Identity.Name.ToString();
            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
            List<TezTuru> turlist = db.TezTuru.OrderBy(b => b.Tur).ToList();
            model.TezTurleriList = (from s in turlist select new SelectListItem { Text = s.Tur, Value = s.Id.ToString() }).ToList();

            List<AkademikBolum> bolumler = db.AkademikBolum.Where(a => a.Id == model.YeniKullanici.AkademikBolumId).OrderBy(b => b.BolumAdi).ToList();
            model.BolumlerList = (from s in bolumler select new SelectListItem { Text = s.BolumAdi, Value = s.Id.ToString() }).ToList();

            List<TezDurumu> durumlar = (from tez in db.Tez join durum in db.TezDurumu on tez.TezDurumId equals durum.Id where tez.DanismanId == model.YeniKullanici.Id select durum).Distinct().ToList();
            model.TezDurumlarıList = (from s in durumlar select new SelectListItem { Text = s.Durum, Value = s.Id.ToString() }).ToList();
            model.SecilenTezlerList = db.Tez.Where(x => x.DanismanId == model.YeniKullanici.Id && x.TezDurumu.Durum == "Devam Ediyor").OrderByDescending(x => x.Id).ToList();

            return View(model);
        }
        [HttpPost]
        public ActionResult DevamEdenTezler(TezOlusturModel t)
        {
            TezOlusturModel model = new TezOlusturModel();
            string e = User.Identity.Name.ToString();
            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
            List<TezTuru> turlist = db.TezTuru.OrderBy(b => b.Tur).ToList();
            model.TezTurleriList = (from s in turlist select new SelectListItem { Text = s.Tur, Value = s.Id.ToString() }).ToList();

            List<AkademikBolum> bolumler = db.AkademikBolum.Where(a => a.Id == model.YeniKullanici.AkademikBolumId).OrderBy(b => b.BolumAdi).ToList();
            model.BolumlerList = (from s in bolumler select new SelectListItem { Text = s.BolumAdi, Value = s.Id.ToString() }).ToList();
            t.YeniKullanici = model.YeniKullanici;
            if (t.Bolum == null && t.TurId == null)
            {
                ViewBag.Uyari = "Lütfen Kriter Seçiniz.";
            }
            else
            {
                if (TezSorgulama.DevamEdenTezSorgula(t).Count != 0)
                {
                    model.SecilenTezlerList = TezSorgulama.DevamEdenTezSorgula(t);
                }
                else
                {
                    ViewBag.Uyari = "İstenilen Kriterde Tez Bulunamadı.";
                }
            }


            return View(model);
        }

        [HttpGet]
        public ActionResult DevamEdenTezDetay(int Id)
        {

            TezOlusturModel model = new TezOlusturModel();
            string e = User.Identity.Name.ToString();
            model.NotlarList = db.Not.Where(x => x.TezId == Id).ToList();
            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
            model.TezToplantilarList = db.Toplanti.Where(x => x.TezId == Id).OrderBy(x => x.Tarih).ToList();
            List<AkademikBolum> bolumlerlist = db.AkademikBolum.OrderBy(b => b.BolumAdi).ToList();
            model.BolumlerList = (from s in bolumlerlist select new SelectListItem { Text = s.BolumAdi, Value = s.Id.ToString() }).ToList();
            List<Dil> Dillerlist = db.Dil.OrderBy(b => b.Id).ToList();
            model.DillerList = (from s in Dillerlist select new SelectListItem { Text = s.Ad, Value = s.Id.ToString() }).ToList();
            List<TezTuru> TurlerList = db.TezTuru.OrderBy(b => b.Id).ToList();
            model.TezTurleriList = (from s in TurlerList select new SelectListItem { Text = s.Tur, Value = s.Id.ToString() }).ToList();

            if (Id != 0)
            {

                model.YeniTez = db.Tez.Where(x => x.Id == Id).FirstOrDefault();
                return View(model);
            }

            return View(model);


        }
        [HttpPost]
        //Guncelle Butonu
        public ActionResult DevamEdenTezDetay(TezOlusturModel t, string yazar1, string yazar2, int Id)
        {
            Bildirim b = new Bildirim();
            TezOlusturModel model = new TezOlusturModel();
            Tez GuncellenecekTez = new Tez();
            if (t.YeniTez.Ad == "" || t.YeniTez.Ad == null || t.YeniTez.AnahtarKelimeler == "")
            {
                TempData["Uyari"] = "Ups! Eksik Bırakılmış Bir Yer Olmadığına Emin Misin?";
                return RedirectToAction("TezDetay");
            }

            string e = User.Identity.Name.ToString();
            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
            GuncellenecekTez = db.Tez.Where(x => x.Id == Id).FirstOrDefault();
            GuncellenecekTez.Ad = t.YeniTez.Ad;
            if ((GuncellenecekTez.OgrenciId1 != null && (t.YeniTez.OgrenciId1 == null || t.YeniTez.OgrenciId1 == 0)) || (GuncellenecekTez.OgrenciId2 != null && (t.YeniTez.OgrenciId2 == null || t.YeniTez.OgrenciId2 == 0)))
            {
                GuncellenecekTez.OgrenciId2 = null;
                GuncellenecekTez.OgrenciId1 = null;
                GuncellenecekTez.TezDurumId = 1;
            }
            if (t.BolumId != 0)
            {
                GuncellenecekTez.AkademikBolumId = t.BolumId;
            }
            if (t.DilId != 0)
            {
                GuncellenecekTez.DilId = t.DilId;
            }
            if (t.TezTurId != 0)
            {
                GuncellenecekTez.TezTuruId = t.TezTurId;
            }
            GuncellenecekTez.TurkceAdi = t.YeniTez.TurkceAdi;

            GuncellenecekTez.AnahtarKelimeler = t.YeniTez.AnahtarKelimeler;
            GuncellenecekTez.Ozet = t.YeniTez.Ozet;


            if (GuncellenecekTez.DilId == 1)
            {

                GuncellenecekTez.TurkceAdi = GuncellenecekTez.Ad;
            }
            else if (GuncellenecekTez.DilId != 1 && GuncellenecekTez.DilId != 0)
            {
                GuncellenecekTez.TurkceAdi = t.YeniTez.TurkceAdi;
            }

            if (ViewBag.Uyari == null)
            {
                if (yazar1 != "" && yazar2 == "")
                {
                    ViewBag.Uyari = KullaniciIslemleri.Sorgula(yazar1);
                    if (ViewBag.Uyari == null)
                    {
                        var yazar = db.Kullanici.Where(i => (i.Ad + " " + i.Soyad).Equals(yazar1) || i.Email.Equals(yazar1)).FirstOrDefault();
                        GuncellenecekTez.OgrenciId1 = yazar.Id;
                        GuncellenecekTez.TezDurumId = 2;
                        b.Aciklama = GuncellenecekTez.Ad + " adlı tezinizin bilgileri " + model.YeniKullanici.Ad + " " + model.YeniKullanici.Soyad + " tarafından güncellendi.";
                        b.Ogrenci1 = yazar.Id;
                        b.DansimanId = model.YeniKullanici.Id;
                        b.TezId = Id;
                        b.Tarih = System.DateTime.Now;
                        db.Bildirim.Add(b);

                    }
                    else
                    {
                        TempData["Uyari"] = "Öğrenci Bilgilerinde Bir Yanlışlık Var!";
                        return Redirect("/Danisman/DevamEdenTezDetay/" + Id + "?#menu1");
                    }
                }
                else if (yazar1 == "" && yazar2 != "")
                {
                    ViewBag.Uyari = KullaniciIslemleri.Sorgula(yazar2);
                    if (ViewBag.Uyari == null)
                    {
                        var yazar = db.Kullanici.Where(i => (i.Ad + " " + i.Soyad).Equals(yazar2) || i.Email.Equals(yazar1)).FirstOrDefault();
                        GuncellenecekTez.OgrenciId1 = yazar.Id;
                        GuncellenecekTez.TezDurumId = 2;
                        b.Aciklama = GuncellenecekTez.Ad + " adlı tezinizin bilgileri " + model.YeniKullanici.Ad + " " + model.YeniKullanici.Soyad + " tarafından güncellendi.";
                        b.Ogrenci1 = yazar.Id;
                        b.DansimanId = model.YeniKullanici.Id;
                        b.TezId = Id;
                        b.Tarih = System.DateTime.Now;
                        db.Bildirim.Add(b);

                    }
                    else
                    {
                        TempData["Uyari"] = "Öğrenci Bilgilerinde Bir Yanlışlık Var!";
                        return Redirect("/Danisman/DevamEdenTezDetay/" + Id + "?#menu1");
                    }
                }
                else if (yazar1 != "" && yazar2 != "")
                {
                    ViewBag.Uyari = KullaniciIslemleri.Sorgula2(yazar1, yazar2);
                    if (ViewBag.Uyari == null)
                    {
                        var y1 = db.Kullanici.Where(i => (i.Ad + " " + i.Soyad).Equals(yazar1) || i.Email.Equals(yazar1)).FirstOrDefault();
                        var y2 = db.Kullanici.Where(i => (i.Ad + " " + i.Soyad).Equals(yazar2) || i.Email.Equals(yazar2)).FirstOrDefault();
                        if (y1 != null && y2 != null)
                        {
                            GuncellenecekTez.OgrenciId1 = y1.Id;
                            GuncellenecekTez.OgrenciId2 = y2.Id;
                            GuncellenecekTez.TezDurumId = 2;
                            b.Aciklama = GuncellenecekTez.Ad + " adlı tezinizin bilgileri " + model.YeniKullanici.Ad + " " + model.YeniKullanici.Soyad + " tarafından güncellendi.";
                            b.Ogrenci1 = y1.Id;
                            b.Ogrenci2 = y2.Id;
                            b.DansimanId = model.YeniKullanici.Id;
                            b.TezId = Id;
                            b.Tarih = System.DateTime.Now;
                            db.Bildirim.Add(b);
                        }
                        else
                        {
                            TempData["Uyari"] = "Öğrenci Bilgilerinde Bir Yanlışlık Var!";
                            return Redirect("/Danisman/DevamEdenTezDetay/" + Id + "?#menu1");
                        }


                    }
                }
                TempData["Basarili"] = "İşleminiz Başarı İle Gerçekleştirildi.";
            }
            db.SaveChanges();
            return Redirect("/Danisman/DevamEdenTezDetay/" + Id + "?#menu1");
        }

        [HttpGet]
        public ActionResult TezDetay(int Id)
        {
            TezOlusturModel model = new TezOlusturModel();
            string e = User.Identity.Name.ToString();
            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
            List<AkademikBolum> bolumlerlist = db.AkademikBolum.OrderBy(b => b.BolumAdi).ToList();
            model.BolumlerList = (from s in bolumlerlist select new SelectListItem { Text = s.BolumAdi, Value = s.Id.ToString() }).ToList();
            List<Dil> Dillerlist = db.Dil.OrderBy(b => b.Id).ToList();
            model.DillerList = (from s in Dillerlist select new SelectListItem { Text = s.Ad, Value = s.Id.ToString() }).ToList();
            List<TezTuru> TurlerList = db.TezTuru.OrderBy(b => b.Id).ToList();
            model.TezTurleriList = (from s in TurlerList select new SelectListItem { Text = s.Tur, Value = s.Id.ToString() }).ToList();

            if (Id != 0)
            {

                model.YeniTez = db.Tez.Where(x => x.Id == Id).FirstOrDefault();
                return View(model);
            }

            return View(model);


        }
        [HttpPost]
        public ActionResult TezDetay(TezOlusturModel t, string yazar1, string yazar2, int Id)
        {
            Bildirim b = new Bildirim();
            TezOlusturModel model = new TezOlusturModel();
            Tez GuncellenecekTez = new Tez();
            if (t.YeniTez.Ad == "" || t.YeniTez.Ad == null || t.YeniTez.AnahtarKelimeler == "")
            {
                TempData["Uyari"] = "Ups! Eksik Bırakılmış Bir Yer Olmadığına Emin Misin?";
                return RedirectToAction("TezDetay");
            }

            string e = User.Identity.Name.ToString();
            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
            GuncellenecekTez = db.Tez.Where(x => x.Id == Id).FirstOrDefault();
            GuncellenecekTez.Ad = t.YeniTez.Ad;
            if ((GuncellenecekTez.OgrenciId1 != null && (t.YeniTez.OgrenciId1 == null || t.YeniTez.OgrenciId1 == 0)) || (GuncellenecekTez.OgrenciId2 != null && (t.YeniTez.OgrenciId2 == null || t.YeniTez.OgrenciId2 == 0)))
            {
                GuncellenecekTez.OgrenciId2 = null;
                GuncellenecekTez.OgrenciId1 = null;
                GuncellenecekTez.TezDurumId = 1;
            }
            if (t.BolumId != 0)
            {
                GuncellenecekTez.AkademikBolumId = t.BolumId;
            }
            if (t.DilId != 0)
            {
                GuncellenecekTez.DilId = t.DilId;
            }
            if (t.TezTurId != 0)
            {
                GuncellenecekTez.TezTuruId = t.TezTurId;
            }
            GuncellenecekTez.TurkceAdi = t.YeniTez.TurkceAdi;

            GuncellenecekTez.AnahtarKelimeler = t.YeniTez.AnahtarKelimeler;
            GuncellenecekTez.Ozet = t.YeniTez.Ozet;


            if (GuncellenecekTez.DilId == 1)
            {

                GuncellenecekTez.TurkceAdi = GuncellenecekTez.Ad;
            }
            else if (GuncellenecekTez.DilId != 1 && GuncellenecekTez.DilId != 0)
            {
                GuncellenecekTez.TurkceAdi = t.YeniTez.TurkceAdi;
            }

            if (ViewBag.Uyari == null)
            {
                if (yazar1 != "" && yazar2 == "")
                {
                    ViewBag.Uyari = KullaniciIslemleri.Sorgula(yazar1);
                    if (ViewBag.Uyari == null)
                    {
                        var yazar = db.Kullanici.Where(i => (i.Ad + " " + i.Soyad).Equals(yazar1) || i.Email.Equals(yazar1)).FirstOrDefault();
                        GuncellenecekTez.OgrenciId1 = yazar.Id;
                        GuncellenecekTez.TezDurumId = 2;
                        b.Aciklama = GuncellenecekTez.Ad + " adlı tezinizin bilgileri " + model.YeniKullanici.Ad + " " + model.YeniKullanici.Soyad + " tarafından güncellendi.";
                        b.Ogrenci1 = yazar.Id;
                        b.DansimanId = model.YeniKullanici.Id;
                        b.TezId = Id;
                        b.Tarih = System.DateTime.Now;
                        db.Bildirim.Add(b);

                    }
                    else
                    {
                        TempData["Uyari"] = "Öğrenci Bilgilerinde Bir Yanlışlık Var!";
                        return RedirectToAction("TezDetay", GuncellenecekTez.Id);
                    }
                }
                else if (yazar1 == "" && yazar2 != "")
                {
                    ViewBag.Uyari = KullaniciIslemleri.Sorgula(yazar2);
                    if (ViewBag.Uyari == null)
                    {
                        var yazar = db.Kullanici.Where(i => (i.Ad + " " + i.Soyad).Equals(yazar2) || i.Email.Equals(yazar1)).FirstOrDefault();
                        GuncellenecekTez.OgrenciId1 = yazar.Id;
                        GuncellenecekTez.TezDurumId = 2;
                        b.Aciklama = GuncellenecekTez.Ad + " adlı tezinizin bilgileri " + model.YeniKullanici.Ad + " " + model.YeniKullanici.Soyad + " tarafından güncellendi.";
                        b.Ogrenci1 = yazar.Id;
                        b.DansimanId = model.YeniKullanici.Id;
                        b.TezId = Id;
                        b.Tarih = System.DateTime.Now;
                        db.Bildirim.Add(b);

                    }
                    else
                    {
                        TempData["Uyari"] = "Öğrenci Bilgilerinde Bir Yanlışlık Var!";
                        return RedirectToAction("TezDetay", GuncellenecekTez.Id);
                    }
                }
                else if (yazar1 != "" && yazar2 != "")
                {
                    ViewBag.Uyari = KullaniciIslemleri.Sorgula2(yazar1, yazar2);
                    if (ViewBag.Uyari == null)
                    {
                        var y1 = db.Kullanici.Where(i => (i.Ad + " " + i.Soyad).Equals(yazar1) || i.Email.Equals(yazar1)).FirstOrDefault();
                        var y2 = db.Kullanici.Where(i => (i.Ad + " " + i.Soyad).Equals(yazar2) || i.Email.Equals(yazar2)).FirstOrDefault();
                        if (y1 != null && y2 != null)
                        {
                            GuncellenecekTez.OgrenciId1 = y1.Id;
                            GuncellenecekTez.OgrenciId2 = y2.Id;
                            GuncellenecekTez.TezDurumId = 2;
                            b.Aciklama = GuncellenecekTez.Ad + " adlı tezinizin bilgileri " + model.YeniKullanici.Ad + " " + model.YeniKullanici.Soyad + " tarafından güncellendi.";
                            b.Ogrenci1 = y1.Id;
                            b.Ogrenci2 = y2.Id;
                            b.DansimanId = model.YeniKullanici.Id;
                            b.TezId = Id;
                            b.Tarih = System.DateTime.Now;
                            db.Bildirim.Add(b);
                        }
                        else
                        {
                            TempData["Uyari"] = "Öğrenci Bilgilerinde Bir Yanlışlık Var!";
                            return RedirectToAction("TezDetay", GuncellenecekTez.Id);
                        }


                    }
                }
                TempData["Basarili"] = "İşleminiz Başarı İle Gerçekleştirildi.";
            }
            db.SaveChanges();
            return RedirectToAction("OlusturulmusTezler");
        }

        [HttpGet]
        public ActionResult Notlar(int Id, string YeniNot)
        {
            TezOlusturModel model = new TezOlusturModel();
            string e = User.Identity.Name.ToString();
            model.NotlarList = (db.Not.Where(x => x.TezId == Id)).OrderByDescending(x => x.Id).ToList();
            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
            if (YeniNot != "" || YeniNot != null)
            {
                Not yeniNot = new Not();
                yeniNot.Aciklama = YeniNot;
                yeniNot.TezId = Id;
                db.Not.Add(yeniNot);

                db.SaveChanges();

            }

            return RedirectToAction("DevamEdenTezDetay", new { Id = Id });
        }
        public ActionResult NotuSil(int Id)
        {
            TempData["Basarili"] = "Notunuz Silindi.";
            TezOlusturModel model = new TezOlusturModel();
            string e = User.Identity.Name.ToString();
            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
            var Sorgu = db.Not.Where(x => x.Id == Id).Select(x => x.TezId).FirstOrDefault();
            int tezId = Convert.ToInt32(Sorgu);
            Not silinecek = db.Not.Where(x => x.Id == Id).FirstOrDefault();
            db.Not.Remove(silinecek);
            db.SaveChanges();
            return Redirect("/Danisman/DevamEdenTezDetay/" + tezId + "?#menu4");
        }
        public ActionResult TezToplanti(int Id)
        {
            TezOlusturModel model = new TezOlusturModel();
            string e = User.Identity.Name.ToString();
            model.TezToplantilarList = db.Toplanti.Where(x => x.TezId == Id).OrderBy(x => x.Tarih).ToList();

            return View();
        }
        public JsonResult TezSil(int Id)
        {

            Tez silinecek = db.Tez.Where(x => x.Id == Id).FirstOrDefault();
            var dizi = db.Bildirim.Where(x => x.TezId == Id).ToList();
            var basvurular = db.TezBasvuru.Where(x => x.TezId == Id).ToList();
            foreach (Bildirim b in dizi)
            {
                db.Bildirim.Remove(b);
            }
            foreach (TezBasvuru b in basvurular)
            {
                db.TezBasvuru.Remove(b);
            }
            TempData["Basarili"] = "İşleminiz Başarı İle Gerçekleştirildi.";
            db.Tez.Remove(silinecek);
            db.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);


        }
        public ActionResult TeziKapat(int id)
        {
            TezOlusturModel model = new TezOlusturModel();
            string e = User.Identity.Name.ToString();
            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
            model.YeniTez = db.Tez.Where(x => x.Id == id).FirstOrDefault();
            model.YeniTez.TezDurumId = 3;
            model.YeniTez.BaslamaTarihi = null;
            db.SaveChanges();
            TempData["Basarili"] = "İşleminiz Başarı İle Gerçekleştirildi.";
            return RedirectToAction("OlusturulmusTezler");
        }
        public ActionResult TeziAc(int id)
        {
            TezOlusturModel model = new TezOlusturModel();
            string e = User.Identity.Name.ToString();
            model.YeniKullanici = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
            model.YeniTez = db.Tez.Where(x => x.Id == id).FirstOrDefault();
            model.YeniTez.TezDurumId = 1;
            db.SaveChanges();
            TempData["Basarili"] = "İşleminiz Başarı İle Gerçekleştirildi.";

            return RedirectToAction("OlusturulmusTezler");
        }
        public ActionResult TezOneriOnayla(int Id, int Ogrenci)
        {
            string e = User.Identity.Name.ToString();
            Kullanici k = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
            OneriTez onaylanan = db.OneriTez.Where(x => x.Id == Id).FirstOrDefault();
            Kullanici o = db.Kullanici.Where(x => x.Id == Ogrenci).FirstOrDefault();
            AkademikBolum bolum = db.AkademikBolum.Where(x => x.Id == o.AkademikBolumId).FirstOrDefault();
            AkademikProgram program = db.AkademikProgram.Where(x => x.Id == bolum.AkademikProgramId).FirstOrDefault();
            AkademikBirim birim = db.AkademikBirim.Where(x => x.Id == program.AkademikBirimId).FirstOrDefault();
            Tez yenitez = new Tez();
            yenitez.OgrenciId1 = Ogrenci;
            yenitez.Ad = onaylanan.Ad;
            if (onaylanan.DilId == 1)
            {
                yenitez.TurkceAdi = yenitez.Ad;
            }
            yenitez.DilId = onaylanan.DilId;
            yenitez.AnahtarKelimeler = onaylanan.AnahtarKelimeler;
            yenitez.Ozet = onaylanan.Ozet;
            yenitez.TezDurumId = 2;
            yenitez.DanismanId = k.Id;
            yenitez.OlusturulmaTarihi = System.DateTime.Now;
            yenitez.TezTuruId = onaylanan.TezTuruId;
            yenitez.AkademikBirimId = birim.Id;
            yenitez.AkademikBolumId = bolum.Id;
            yenitez.AkademikProgramId = program.Id;

            onaylanan.BasvuruDurumuId = 2;

            Bildirim bildirim = new Bildirim();
            bildirim.Aciklama = onaylanan.Ad + " adlı tez önerin " + k.Ad + " " + k.Soyad + " tarafından onaylanmıştır.";
            bildirim.Ogrenci1 = Ogrenci;
            bildirim.DansimanId = k.Id;
            bildirim.Tarih = System.DateTime.Now;
            bildirim.OneriTezId = Id;
            db.Tez.Add(yenitez);
            db.Bildirim.Add(bildirim);
            db.SaveChanges();



            return RedirectToAction("OlusturulmusTezler");
        }
        public ActionResult TezOneriReddet(int Id, int Ogrenci)
        {
            string e = User.Identity.Name.ToString();
            Kullanici k = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
            OneriTez reddedilen = db.OneriTez.Where(x => x.Id == Id).FirstOrDefault();
            Bildirim bildirim = new Bildirim();
            bildirim.Aciklama = reddedilen.Ad + " adlı tez önerin " + k.Ad + " " + k.Soyad + " tarafından reddedildi.";
            bildirim.Ogrenci1 = Ogrenci;
            bildirim.DansimanId = k.Id;
            bildirim.Tarih = System.DateTime.Now;
            bildirim.OneriTezId = Id;
            reddedilen.BasvuruDurumuId = 3;
            db.Bildirim.Add(bildirim);
            db.SaveChanges();



            TempData["Bilgi"] = "Tez Önerisi Reddedildi.";
            return RedirectToAction("TezOnerileri");
        }

        public ActionResult TalepOnayla(int Id, int Ogrenci, int Talep)
        {
            string e = User.Identity.Name.ToString();
            Kullanici k = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();

            var tez = db.Tez.Where(a => a.Id == Id).FirstOrDefault();
            var talep = db.TezBasvuru.Where(a => a.Id == Talep).FirstOrDefault();
            tez.OgrenciId1 = Ogrenci;
            tez.TezDurumId = 2;
            Bildirim bildirim = new Bildirim();
            bildirim.DansimanId = k.Id;
            bildirim.Ogrenci1 = Ogrenci;
            bildirim.Tarih = System.DateTime.Now;
            bildirim.TezId = Id;
            bildirim.Aciklama = tez.Ad + " isimli tez talebin onaylandı.";
            bildirim.Okundu_Okunmadı = false;
            talep.BasvuruDurumuId = 2;
            db.Bildirim.Add(bildirim);
            db.SaveChanges();

            return RedirectToAction("TezTalepleri");
        }
        public ActionResult TalepReddet(int Id, int Ogrenci, int Talep)
        {
            string e = User.Identity.Name.ToString();
            Kullanici k = db.Kullanici.Where(x => x.Email == e).FirstOrDefault();
            Kullanici o = db.Kullanici.Where(x => x.Id == Ogrenci).FirstOrDefault();
            var tez = db.Tez.Where(a => a.Id == Id).FirstOrDefault();
            var talep = db.TezBasvuru.Where(a => a.Id == Talep).FirstOrDefault();

            Bildirim bildirim = new Bildirim();
            bildirim.DansimanId = k.Id;
            bildirim.Ogrenci1 = Ogrenci;
            bildirim.Tarih = System.DateTime.Now;
            bildirim.TezId = Id;
            bildirim.Aciklama = tez.Ad + " isimli tez talebin reddedildi.";
            bildirim.Okundu_Okunmadı = false;
            talep.BasvuruDurumuId = 3;
            db.Bildirim.Add(bildirim);
            db.SaveChanges();

            return RedirectToAction("TezTalepleri");
        }





    }
}