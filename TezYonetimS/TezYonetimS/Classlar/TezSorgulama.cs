using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TezYonetimSis.Models;

namespace TezYonetimSis.Classlar
{
    public class TezSorgulama
    {
       
        public static List<TezBasvuru> TalepSorgu(int tez_id, int teztur_id,int danismanid)
        {
            TezYonetimSistemiEntities db = new TezYonetimSistemiEntities();
            List<TezBasvuru> sorgu;
            if (tez_id != 0 && teztur_id==0)
            {
                sorgu = (from t in db.Tez
                         join basvuru in db.TezBasvuru on t.Id equals basvuru.TezId
                         where t.DanismanId == danismanid && t.Id == tez_id && basvuru.BasvuruDurumu.Durum == "Beklemede" select basvuru).ToList();
            }
            else if(teztur_id!= 0&&tez_id == 0 )
            {
                sorgu = (from t in db.Tez
                         join basvuru in db.TezBasvuru on t.Id equals basvuru.TezId
                         where t.DanismanId == danismanid && t.TezTuruId == teztur_id && basvuru.BasvuruDurumu.Durum == "Beklemede"
                         select basvuru).ToList();
            }
            else
            {  
                    sorgu = (from t in db.Tez
                             join basvuru in db.TezBasvuru on t.Id equals basvuru.TezId
                             where t.DanismanId == danismanid && t.TezTuruId == teztur_id && t.Id == tez_id && basvuru.BasvuruDurumu.Durum == "Beklemede"
                             select basvuru).ToList();
            }

            return sorgu;
           
        }

        public static List<Tez> OlusturulmusTezSorgulama(TezOlusturModel t)
        {
            TezYonetimSistemiEntities db = new TezYonetimSistemiEntities();
            List<Tez> Sorgu = null;
                if (t.Bolum!=null && t.TurId != null)
                {
                   
                    int bolum_Id = Int32.Parse(t.Bolum);
                    int tur_Id = Int32.Parse(t.TurId);
                    Sorgu = (from tez in db.Tez where tez.DanismanId == t.YeniKullanici.Id && tez.AkademikBolumId == bolum_Id && tez.TezTuruId == tur_Id && (tez.TezDurumu.Durum == "Açık" || tez.TezDurumu.Durum == "Kapalı") orderby tez.OlusturulmaTarihi descending select tez).ToList();
                     return Sorgu;
                }
              
                else if (t.Bolum == null && t.TurId != null)
                {
                    
                
                    int tur_Id = Int32.Parse(t.TurId);
                    Sorgu = (from tez in db.Tez where tez.DanismanId == t.YeniKullanici.Id  && tez.TezTuruId == tur_Id && (tez.TezDurumu.Durum == "Açık" || tez.TezDurumu.Durum == "Kapalı") orderby tez.OlusturulmaTarihi descending select tez).ToList();
                     return Sorgu;
                }
                else if ( t.Bolum != null && t.TurId == null)
                {
                    
                    int bolum_Id = Int32.Parse(t.Bolum);
                
                    Sorgu = (from tez in db.Tez where tez.DanismanId == t.YeniKullanici.Id && tez.AkademikBolumId == bolum_Id && (tez.TezDurumu.Durum == "Açık" || tez.TezDurumu.Durum == "Kapalı") orderby tez.OlusturulmaTarihi descending select tez).ToList();
                     return Sorgu;
                }
                else if ( t.Bolum == null && t.TurId == null)
                {
                   
                
                    Sorgu = (from tez in db.Tez where tez.DanismanId == t.YeniKullanici.Id && (tez.TezDurumu.Durum == "Açık" || tez.TezDurumu.Durum == "Kapalı") orderby tez.OlusturulmaTarihi descending select tez).ToList();
                
                     return Sorgu;
                }
           
            return Sorgu;
        }
        public static List<Tez> DevamEdenTezSorgula(TezOlusturModel t)
        {
            TezYonetimSistemiEntities db = new TezYonetimSistemiEntities();
            List<Tez> Sorgu = null;
            if (t.Bolum != null && t.TurId != null)
            {

                int bolum_Id = Int32.Parse(t.Bolum);
                int tur_Id = Int32.Parse(t.TurId);
                Sorgu = (from tez in db.Tez where tez.DanismanId == t.YeniKullanici.Id && tez.AkademikBolumId == bolum_Id && tez.TezTuruId == tur_Id && tez.TezDurumu.Durum == "Devam Ediyor" orderby tez.OlusturulmaTarihi descending select tez).ToList();
                return Sorgu;
            }

            else if (t.Bolum == null && t.TurId != null)
            {


                int tur_Id = Int32.Parse(t.TurId);
                Sorgu = (from tez in db.Tez where tez.DanismanId == t.YeniKullanici.Id && tez.TezTuruId == tur_Id && tez.TezDurumu.Durum == "Devam Ediyor" orderby tez.OlusturulmaTarihi descending select tez).ToList();
                return Sorgu;
            }
            else if (t.Bolum != null && t.TurId == null)
            {

                int bolum_Id = Int32.Parse(t.Bolum);

                Sorgu = (from tez in db.Tez where tez.DanismanId == t.YeniKullanici.Id && tez.AkademikBolumId == bolum_Id && tez.TezDurumu.Durum == "Devam Ediyor" orderby tez.OlusturulmaTarihi descending select tez).ToList();
                return Sorgu;
            }
            else if (t.Bolum == null && t.TurId == null)
            {


                Sorgu = (from tez in db.Tez where tez.DanismanId == t.YeniKullanici.Id && tez.TezDurumu.Durum == "Devam Ediyor" orderby tez.OlusturulmaTarihi descending select tez).ToList();

                return Sorgu;
            }

            return Sorgu;
        }

        public static List<OneriTez> OneriTezSorgula(TezOlusturModel t) 
        {
            TezYonetimSistemiEntities db = new TezYonetimSistemiEntities();
           
            List<OneriTez> Sorgu=null;
            
            if(t.Bolum!=null && t.TurId != null)
            {
                int bolum_Id = Int32.Parse(t.Bolum);
                int tur_Id = Int32.Parse(t.TurId);
                Sorgu=( from oneri in db.OneriTez where oneri.TezTuruId==tur_Id && oneri.DanismanId==t.YeniKullanici.Id && oneri.Kullanici.AkademikBolumId==bolum_Id && oneri.BasvuruDurumuId==1 select oneri).ToList();
                return Sorgu;
            }
            else if (t.Bolum == null && t.TurId != null)
            {
                int tur_Id = Int32.Parse(t.TurId);
                Sorgu = (from oneri in db.OneriTez where oneri.TezTuruId == tur_Id && oneri.DanismanId == t.YeniKullanici.Id && oneri.BasvuruDurumuId == 1 select oneri).ToList();
                return Sorgu;
            }
            else if (t.Bolum != null && t.TurId == null)
            {
                int bolum_Id = Int32.Parse(t.Bolum);
                Sorgu = (from oneri in db.OneriTez where oneri.DanismanId == t.YeniKullanici.Id && oneri.Kullanici.AkademikBolumId == bolum_Id && oneri.BasvuruDurumuId == 1 select oneri).ToList();
                return Sorgu;
            }
            return Sorgu;
        }

    }
}