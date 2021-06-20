using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TezYonetimS.Models;

namespace TezYonetimS.Controllers
{
    public class HomeController : Controller
    {

        TezYonetimSistemiEntities db = new TezYonetimSistemiEntities();
        [HttpGet]
        public ActionResult Index()
        {
            Akademikturlermodel model = new Akademikturlermodel();
            List<AkademikBirim> birimlist = db.AkademikBirim.OrderBy(b => b.BirimAdi).ToList();
            model.BirimlerList = (from s in birimlist select new SelectListItem { Text = s.BirimAdi, Value = s.Id.ToString() }).ToList();
            List<Dil> Dillerlist = db.Dil.OrderBy(b => b.Id).ToList();
            model.DillerList = (from s in Dillerlist select new SelectListItem { Text = s.Ad, Value = s.Id.ToString() }).ToList();

            return View(model);
        }


        [HttpPost]
        public ActionResult Index(Akademikturlermodel k)
        {
            Akademikturlermodel model = new Akademikturlermodel();
            List<AkademikBirim> birimlist = db.AkademikBirim.OrderBy(b => b.BirimAdi).ToList();
            model.BirimlerList = (from s in birimlist select new SelectListItem { Text = s.BirimAdi, Value = s.Id.ToString() }).ToList();
            List<Dil> Dillerlist = db.Dil.OrderBy(b => b.Id).ToList();
            model.DillerList = (from s in Dillerlist select new SelectListItem { Text = s.Ad, Value = s.Id.ToString() }).ToList();
            Tez t = new Tez();


            if (k.BirimId > 0 && k.BolumId > 0 && k.ProgramId > 0)
            {
                t.AkademikBirimId = k.BirimId;
                t.AkademikBolumId = k.BolumId;
                t.AkademikProgramId = k.ProgramId;
                if (k.DanismanId > 0 && k.TezTurId > 0 && k.aranacakterim != null && k.aranacakalan != null)
                {
                    t.AkademikProgramId = k.ProgramId;
                    t.TezTuruId = k.TezTurId;
                    t.AnahtarKelimeler = k.aranacakterim;


                }
                else if (k.DanismanId > 0 && k.TezTurId > 0 && k.aranacakterim != null && k.aranacakalan != null)
                {
                    t.TezTuruId = k.TezTurId;
                    t.AnahtarKelimeler = k.aranacakterim;
                }
                else if (k.DanismanId <= 0 && k.TezTurId <= 0 && k.aranacakterim != null && k.aranacakalan != null)
                {

                    t.AnahtarKelimeler = k.aranacakterim;
                }
                else if ((k.DanismanId <= 0 && k.TezTurId <= 0 && k.aranacakterim == null && k.aranacakalan != null) || (k.DanismanId <= 0 && k.TezTurId <= 0 && k.aranacakterim != null && k.aranacakalan == null))
                {
                    ViewBag.Uyarı = "Arama yapılacak terim alanını ya da aranacak alanı boş bırakmış olabilir misin?";
                }


            }
            else
            {
                ViewBag.Uyari = "Akademik birim, program ya da bölümden birisini seçmeyi atlamış olabilir misin?";
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
        public JsonResult DanismanListele(int id)
        {

            List<Kullanici> danismanlist = db.Kullanici.Where(p => p.AkademikBolumId == id && p.RolId == 2).OrderBy(p => p.Ad).ToList();
            List<SelectListItem> itemList = (from i in danismanlist select new SelectListItem { Value = i.Id.ToString(), Text = i.Ad + " " + i.Soyad }).ToList();
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


    }

}
