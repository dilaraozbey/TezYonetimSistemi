using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TezYonetimS.Models;

namespace TezYonetimS.Classlar
{

    public class KullaniciIslemleri
    {
        public static string uyari;
        public static bool SayiMi(string text)
        {
            foreach (char chr in text)
            {
                if (!Char.IsNumber(chr)) return false;
            }
            return true;
        }
        public static string YeniKullanici(Akademikturlermodel k, string sifreDogrulama)
        {
            uyari = null;
            TezYonetimSistemiEntities db = new TezYonetimSistemiEntities();
            var uye = db.Kullanici.Where(i => i.Email.Equals(k.YeniKullanici.Email)).FirstOrDefault();
            if (k.YeniKullanici.Ad != null && k.YeniKullanici.Soyad != null && k.YeniKullanici.Sifre != null && k.YeniKullanici.Email != null && sifreDogrulama != null && k.ProgramId != 0 && k.BolumId != 0 && k.BirimId != 0)
            {
                if (uye != null)
                {
                    uyari = "Hey!! Daha Önce Üye Olmadığına Emin Misin?";


                }

                else if (k.YeniKullanici.Sifre != sifreDogrulama)
                {
                    uyari = "Ups!!! Şifreler Uyuşmuyor.";

                }
                else
                {
                    uyari = null;
                }
            }

            else if (k.YeniKullanici.Ad == null || k.YeniKullanici.Soyad == null && k.YeniKullanici.Sifre == null || k.YeniKullanici.Email == null || sifreDogrulama == "" || k.ProgramId == 0 || k.BolumId == 0 || k.BirimId == 0)
            {
                uyari = "Gözden Kaçırdığın Bir Yer Olmadığına Emin Misin?";


            }
            else
            {
                uyari = null;
            }
            return uyari;

        }
        public static string YeniTezOlustur(TezOlusturModel t)
        {
            TezYonetimSistemiEntities db = new TezYonetimSistemiEntities();
            var sorgu = db.Tez.Where(i => i.Ad.Equals(t.YeniTez.Ad)).FirstOrDefault();

            if (sorgu != null)
            {
                uyari = "Üzgünüm:( Bu Tez Adı Daha Önce Kullanılmış!";
            }
            else
            {
                uyari = null;
            }


            return uyari;
        }
        public static string TurkceMi(TezOlusturModel t)
        {
            TezYonetimSistemiEntities db = new TezYonetimSistemiEntities();
            var seciledil = db.Dil.Where(i => i.Id.Equals(t.DilId)).FirstOrDefault();
            if ((t.YeniTez.TurkceAdi == "" && seciledil.Ad == "Türkçe") || (t.YeniTez.TurkceAdi == null && seciledil.Ad == "Türkçe"))
            {
                t.YeniTez.TurkceAdi = t.YeniTez.Ad;
            }

            return t.YeniTez.TurkceAdi;
        }
        public static string Sorgula(string yazar1)
        {
            TezYonetimSistemiEntities db = new TezYonetimSistemiEntities();
            var yazar = db.Kullanici.Where(i => (i.Ad + " " + i.Soyad).Equals(yazar1) || i.Email.Equals(yazar1)).FirstOrDefault();

            if (yazar == null)
            {
                uyari = "Böyle Bir Öğrenci Olduğuna Emin Misin?";
            }
            else
            {
                uyari = null;
            }

            return uyari;
        }
        public static string Sorgula2(string yazar1, string yazar2)
        {
            TezYonetimSistemiEntities db = new TezYonetimSistemiEntities();
            var y1 = db.Kullanici.Where(i => (i.Ad + " " + i.Soyad).Equals(yazar1) || i.Email.Equals(yazar1)).FirstOrDefault();
            var y2 = db.Kullanici.Where(i => (i.Ad + " " + i.Soyad).Equals(yazar1) || i.Email.Equals(yazar2)).FirstOrDefault();
            if (y1 == null || y2 == null)
            {
                uyari = "Öğrenci Bilgilerini Tekrar Kontrol Eder Misin?";
            }
            else
            {
                uyari = null;
            }

            return uyari;
        }


    }
}