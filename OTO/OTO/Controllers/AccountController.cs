using OTO.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace OTO.Controllers
{

    public class AccountController : Controller
    {
        SanOtoEntities db = new SanOtoEntities();

        // GET: /Account/Login


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["KullaniciAdi"] = null;
            return RedirectToAction("Login", "Account");
        }



        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }



        // POST: /Account/Login
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Kullanici model)
        {
            var kullaniciID = db.Kullanici.FirstOrDefault(x => x.k_eposta == model.k_eposta && x.k_sifre == model.k_sifre);

            if (kullaniciID != null)
            {
                FormsAuthentication.SetAuthCookie(kullaniciID.k_eposta, false);
                Session["KullaniciAdi"] = kullaniciID;

                var ads = kullaniciID.k_id;

                ViewBag.ID = ads;


                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Mesaj = "Geçersiz Eposta veya Şifre ";
                return View();
            }
        }

        //



        // KULLANİCİ EKLE
        [HttpGet]
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Register(Kullanici p1)
        {
            db.Kullanici.Add(p1);

            db.SaveChanges();
            return RedirectToAction("Login", "Account");

        }
        // KULLANİCİ EKLE BİTİŞ




        // PROFİL GUNCELLE 

        public ActionResult Profil()
        {
            var a = db.Kullanici.Where(x => x.k_eposta == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().k_id;

            ViewBag.ProfilID = a;

            var deger = db.Kullanici.ToList();
            return View(deger);
        }


        // PROFİL GUNCELLE BİTİŞ



        // PAROLAMI UNUTTUM 


        [AllowAnonymous]
        public ActionResult ParolamiUnuttum()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ParolamiUnuttum(Kullanici model)
        {
            try
            {
               // Kullanici kul = db.Kullanici.Find(model.k_id);

                var kullaniciID = db.Kullanici.FirstOrDefault(x => x.k_eposta == model.k_eposta);
                
                if (kullaniciID != null)
                {
                    kullaniciID.k_sifre = model.k_sifre;

                    db.SaveChanges();

                    ViewBag.Message = "İşlem Tamam Login' e Yönlendiriliyor...";


                    return RedirectToAction("Login");
                }
            }
            catch (Exception)
            {

                ViewBag.Mesaj = "Hata Oluştu !!";



                throw;
            }
            return View();


        }

        // PAROLAMI UNUTTUM BİTİŞ





    }
}