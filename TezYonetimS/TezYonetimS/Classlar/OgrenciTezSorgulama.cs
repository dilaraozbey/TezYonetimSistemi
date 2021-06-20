using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TezYonetimSis.Classlar
{
    public class OgrenciTezSorgulama
    {
        
        public static List<Tez> TezSorgula(string TezAdi, int DanismanId, string aranacakterim)
        {   
            TezYonetimSistemiEntities db = new TezYonetimSistemiEntities();
            List<Tez> sorgusonucu;
            if (TezAdi != "" && DanismanId != 0 && aranacakterim!="")
            {

                sorgusonucu = db.Tez.Where(p => p.Ad.Contains(TezAdi) && p.DanismanId == DanismanId && p.AnahtarKelimeler.Contains(aranacakterim) && p.TezDurumu.Durum == "Açık").OrderBy(p => p.OlusturulmaTarihi).ToList();

                return sorgusonucu;

            }
            else if (TezAdi == "" && DanismanId != 0 && aranacakterim != "")
            {

                sorgusonucu = db.Tez.Where(p => p.DanismanId == DanismanId && p.AnahtarKelimeler.Contains(aranacakterim) && p.TezDurumu.Durum == "Açık").OrderBy(p => p.OlusturulmaTarihi).ToList();
                return sorgusonucu;
            }
            else if (TezAdi != "" && DanismanId == 0 && aranacakterim != "")
            {

                sorgusonucu = db.Tez.Where(p => p.Ad.Contains(TezAdi) && p.AnahtarKelimeler.Contains(aranacakterim) && p.TezDurumu.Durum == "Açık").OrderBy(p => p.OlusturulmaTarihi).ToList();
                return sorgusonucu;
            }
           
            else if (TezAdi != "" && DanismanId ==0 && aranacakterim == "")
            {

                sorgusonucu = db.Tez.Where(p => p.Ad.Contains(TezAdi) && p.TezDurumu.Durum == "Açık").OrderBy(p => p.OlusturulmaTarihi).ToList();
                return sorgusonucu;
            }
            else if (TezAdi != "" && DanismanId != 0 && aranacakterim == "")
            {

                sorgusonucu = db.Tez.Where(p => p.Ad.Contains(TezAdi) && p.DanismanId == DanismanId && p.TezDurumu.Durum == "Açık").OrderBy(p => p.OlusturulmaTarihi).ToList();
                return sorgusonucu;
            }
            else if (TezAdi == "" && DanismanId != 0 && aranacakterim == "")
            {

                sorgusonucu = db.Tez.Where(p => p.DanismanId == DanismanId && p.TezDurumu.Durum == "Açık").OrderBy(p => p.OlusturulmaTarihi).ToList();
                return sorgusonucu;
            }
            else //(TezAdi == "" && DanismanId == 0 && aranacakterim != "")//
            {

                sorgusonucu = db.Tez.Where(p => p.AnahtarKelimeler.Contains(aranacakterim) && p.TezDurumu.Durum == "Açık").OrderBy(p => p.OlusturulmaTarihi).ToList();
                return sorgusonucu;
            }
           
        }
    }
}