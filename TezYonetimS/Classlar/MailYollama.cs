using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Configuration;

namespace TezYonetimS.Classlar
{
    public class MailYollama
    {
      
            public static void SendActivationEmail(Kullanici KBilgi)
            {

                
                string EpostaSunucu = ConfigurationManager.AppSettings["EpostaSunucu"].ToString();
                string EpostaPort = ConfigurationManager.AppSettings["EpostaPort"].ToString();
                string EpostaAdi = ConfigurationManager.AppSettings["EpostaAdi"].ToString();
                string Eposta = ConfigurationManager.AppSettings["Eposta"].ToString();
                string EpostaSifre = ConfigurationManager.AppSettings["EpostaSifre"].ToString();
                SmtpClient smtp = new SmtpClient();
                smtp.Host = EpostaSunucu;
                smtp.Port = Convert.ToInt32(EpostaPort);
                smtp.EnableSsl = true;
                NetworkCredential kullanicibilgi = new NetworkCredential(Eposta, EpostaSifre);
                smtp.Credentials = kullanicibilgi;
                MailAddress Gonderen = new MailAddress(Eposta);
                MailAddress Alici = new MailAddress("dilaraozbeyy@yahoo.com");
                MailMessage Mail = new MailMessage(Gonderen, Alici);
                     Mail.Subject = "Hesap Aktivasyonu";
                    string body = "Merhaba " + KBilgi.Ad + " " + KBilgi.Soyad + ",";
                    body += "<br /><br />Seni aramızda görmek çok güzel!:) Hesabını etkinleştirmek için lütfen aşağıdaki bağlantıya tıkla.";
                    body += "<br /><a href = '" + string.Format("{0}://{1}/Kullanici/Aktifet/{2}", "https", "localhost:44306", KBilgi.Kod1 + KBilgi.Kod2) + "'>Hesabınızı etkinleştirmek için burayı tıklayın.</a>";
                    body += "<br /><br />Teşekkürler.";
                    //body += "<br /><br /> <img src=\"Uploads/www/images/Yalova-U%CC%88niversitesi-Logo.png.png\"  width=\"100\" height=\"20\"> "
                    Mail.Body = body;
                    Mail.IsBodyHtml = true;
                   
                    smtp.Send(Mail);
                
            }
        
    }
}