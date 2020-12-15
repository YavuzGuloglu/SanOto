using Microsoft.AspNet.Identity;
using OTO.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace OTO.Controllers
{
    public class HomeLoginController : Controller
    {
        // GET: HomeLogin

        SanOtoEntities db = new SanOtoEntities();


        // ILAN LİSTELEME

        [Authorize(Roles = "False")]
        public ActionResult IlanList()
        {
            var ilan = db.Kullanici.Where(x => x.k_eposta == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().k_id;

            ViewBag.ilanID = ilan;

            var deger = db.Ilan.ToList();
            return View(deger);
        }

        // ILAN LİSTELEME BİTİŞ



        // ILAN EKLE 

        [Authorize(Roles = "False")]
        [HttpGet]
        public ActionResult IlanEkle()
        {

            List<SelectListItem> kategori = (from i in db.Kategori.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.kategori_ad,
                                                 Value = i.kategori_ID.ToString()
                                             }).ToList();
            ViewBag.ktgr = kategori;

            return View();
        }


        [Authorize(Roles = "False")]
        [HttpPost]
        public ActionResult IlanEkle(Ilan p1, HttpPostedFileBase yuklenecekresim)
        {

            try
            {
                var kgt = db.Kategori.Where(x => x.kategori_ID == p1.Kategori.kategori_ID).FirstOrDefault();
                p1.Kategori = kgt;
                var ext = Path.GetExtension(yuklenecekresim.FileName);

                var dosyaAdi = Guid.NewGuid().ToString() + ext;
                var yuklemeYeri = Path.Combine(Server.MapPath("~/Content/img/ilan/"), dosyaAdi.ToString());
                yuklenecekresim.SaveAs(yuklemeYeri);


                var a = db.Kullanici.Where(x => x.k_eposta == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().k_id;


                p1.ilan_kullanici = a;

                p1.ilan_foto = dosyaAdi;
                db.Ilan.Add(p1);
                db.SaveChanges();
                Response.Write("İşlem Tamam!");
                return RedirectToAction("IlanList");



            }
            catch (Exception)
            {
                ViewBag.Hata = "Bir şeyler yanlış gitti..!";

                return View();
            }

        }

        // ILAN EKLE BİTİŞ


        // ILAN SİL 

        public ActionResult IlanSil(int id)
        {
            var ilan = db.Ilan.Find(id);
            db.Ilan.Remove(ilan);
            db.SaveChanges();

            if (ilan.ilan_foto != null)
            {
                string resyol = "~/Content/img/ilan/";
                System.IO.File.Delete(Server.MapPath(resyol + ilan.ilan_foto));
            }

            return RedirectToAction("IlanList");
        }

        // ILAN SİL BİTİŞ



        // ILAN GUNCELLE

        public ActionResult IlanGuncelle(int id)
        {
            var ilan = db.Ilan.Find(id);
            List<SelectListItem> deger = (from i in db.Kategori.ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.kategori_ad,
                                              Value = i.kategori_ID.ToString()

                                          }).ToList();
            ViewBag.deger = deger;

            return View("IlanGuncelle", ilan);
        }


        public ActionResult IlanGuncel(Ilan p1, HttpPostedFileBase guncellenecekResimIlan)
        {
            Ilan il = db.Ilan.Find(p1.ilan_id);
            if (il.ilan_foto != null)
            {
                string resyol = "~/Content/img/ilan/";
                System.IO.File.Delete(Server.MapPath(resyol + il.ilan_foto));
            }

            if (guncellenecekResimIlan != null)
            {
                var ext = Path.GetExtension(guncellenecekResimIlan.FileName);
                var dosyaAd = Guid.NewGuid().ToString() + ext;
                var dosyaYer = Path.Combine(Server.MapPath("~/Content/img/ilan/"), dosyaAd);
                guncellenecekResimIlan.SaveAs(dosyaYer);

                p1.ilan_foto = dosyaAd;
            }

            
            il.ilan_foto = p1.ilan_foto;
            il.ilan_adres = p1.ilan_adres;
            il.ilan_baslik = p1.ilan_baslik;
            il.ilan_detay = p1.ilan_detay;
            il.ilan_fiyat = p1.ilan_fiyat;
            il.ilan_ilce = p1.ilan_ilce;
            il.ilan_kategori = p1.ilan_kategori;
            il.ilan_sehir = p1.ilan_sehir;
            il.ilan_tarih = p1.ilan_tarih;
            il.ilan_verenAd = p1.ilan_verenAd;
            il.ilan_verenEposta = p1.ilan_verenEposta;
            il.ilan_verenSoyad = p1.ilan_verenSoyad;
            il.ilan_verenTel = p1.ilan_verenTel;

            db.SaveChanges();


            return RedirectToAction("IlanList");
        }

        // ILAN GUNCELLE BİTİŞ




        // Usta List

        [Authorize(Roles = "True")]
        public ActionResult UstaList()
        {
            var a = db.Kullanici.Where(x => x.k_eposta == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().k_id;

            ViewBag.ID = a;

            var deger = db.Firma.ToList();
            return View(deger);
        }

        // Usta List BiTİŞ




        // Usta EKLE 

        [Authorize(Roles = "True")]
        [HttpGet]
        public ActionResult UstaEkle()
        {

            List<SelectListItem> kategori = (from i in db.Kategori.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.kategori_ad,
                                                 Value = i.kategori_ID.ToString()
                                             }).ToList();
            ViewBag.ktgr = kategori;

            List<SelectListItem> gun = (from i in db.Gunler.ToList()
                                        select new SelectListItem
                                        {
                                            Text = i.gunler1,
                                            Value = i.gun_id.ToString()
                                        }).ToList();
            ViewBag.gun = gun;


            return View();
        }


        [Authorize(Roles = "True")]
        [HttpPost]
        public ActionResult UstaEkle(Firma p1, HttpPostedFileBase yuklenecekresim, HttpPostedFileBase yuklenecekresim2)
        {

            try
            {
                var kgt = db.Kategori.Where(x => x.kategori_ID == p1.Kategori.kategori_ID).FirstOrDefault();
                var gun = db.Gunler.Where(x => x.gun_id == p1.Gunler.gun_id).FirstOrDefault();
                p1.Kategori = kgt;
                p1.Gunler = gun;

                var ext = Path.GetExtension(yuklenecekresim.FileName);

                var dosyaAdi = Guid.NewGuid().ToString() + ext;
                var yuklemeYeri = Path.Combine(Server.MapPath("~/Content/img/usta/"), dosyaAdi.ToString());
                yuklenecekresim.SaveAs(yuklemeYeri);
              
                var ext2 = Path.GetExtension(yuklenecekresim2.FileName);

                var dosyaAdi2 = Guid.NewGuid().ToString() + ext2;
                var yuklemeYeri2 = Path.Combine(Server.MapPath("~/Content/img/usta/profil/"), dosyaAdi2.ToString());
                yuklenecekresim2.SaveAs(yuklemeYeri2);


                var a = db.Kullanici.Where(x => x.k_eposta == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().k_id;
                p1.f_kullanici = a;


                p1.f_foto = dosyaAdi;
                p1.f_ustaFoto = dosyaAdi2;

                db.Firma.Add(p1);
                db.SaveChanges();
                Response.Write("İşlem Tamam!");
                return RedirectToAction("UstaList");


            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }

        }

        // Usta EKLE BiTİŞ



        // Usta SİL 

        public ActionResult UstaSil(int id)
        {
            var usta = db.Firma.Find(id);
            db.Firma.Remove(usta);
            db.SaveChanges();

            if (usta.f_foto != null)
            {
                string resyol = "~/Content/img/usta/";
                System.IO.File.Delete(Server.MapPath(resyol + usta.f_foto));
            }

            return RedirectToAction("UstaList");
        }

        // USTA SİL BİTİŞ



        // USTA GUNCELLE

        public ActionResult UstaGuncelle(int id)
        {
            var usta = db.Firma.Find(id);
            List<SelectListItem> ustakategori = (from i in db.Kategori.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = i.kategori_ad,
                                                     Value = i.kategori_ID.ToString()
                                                 }).ToList();
            ViewBag.ustakategori = ustakategori;



            List<SelectListItem> ustagun = (from i in db.Gunler.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.gunler1,
                                                Value = i.gun_id.ToString()
                                            }).ToList();
            ViewBag.ustagun = ustagun;


            return View("UstaGuncelle", usta);
        }


        public ActionResult UstaGuncel(Firma p1, HttpPostedFileBase guncellenecekResim1, HttpPostedFileBase guncellenecekResim2)
        {                                                               // f_ustaFoto                                //f_foto               
            var fir = db.Firma.Find(p1.f_id);

            if (fir.f_foto != null)
            { string resyol = "~/Content/img/usta/"; System.IO.File.Delete(Server.MapPath(resyol + fir.f_foto)); }
                  
            if (fir.f_ustaFoto != null)
            {string resyol = "~/Content/img/usta/profil/"; System.IO.File.Delete(Server.MapPath(resyol + fir.f_foto));}

            if (guncellenecekResim1 != null)
            {
                var ext1 = Path.GetExtension(guncellenecekResim1.FileName);
                var dosyaAd1= Guid.NewGuid().ToString() + ext1;
                var dosyaYer1 = Path.Combine(Server.MapPath("~/Content/img/usta/profil/"), dosyaAd1);
                guncellenecekResim1.SaveAs(dosyaYer1);

                p1.f_ustaFoto = dosyaAd1;
            }

            if (guncellenecekResim2 != null)
            {
                var ext2 = Path.GetExtension(guncellenecekResim2.FileName);
                var dosyaAd2 = Guid.NewGuid().ToString() + ext2;
                var dosyaYer2 = Path.Combine(Server.MapPath("~/Content/img/usta/"), dosyaAd2);
                guncellenecekResim2.SaveAs(dosyaYer2);

                p1.f_foto = dosyaAd2;
            }


            fir.f_foto = p1.f_foto;
            fir.f_ustaFoto = p1.f_ustaFoto;
            fir.f_adresDetay = p1.f_adresDetay;
            fir.f_ad = p1.f_ad;
            fir.f_detay = p1.f_detay;
            fir.f_fiyat = p1.f_fiyat;
            fir.f_ilce = p1.f_ilce;
            fir.f_kategori = p1.f_kategori;
            fir.f_sehir = p1.f_sehir;
            fir.f_calismagun = p1.f_calismagun;
            fir.f_yetkiliAdSoyad = p1.f_yetkiliAdSoyad;
            fir.f_epota = p1.f_epota;
            fir.f_calismaSaat = p1.f_calismaSaat;
            fir.f_sirketTel = p1.f_sirketTel;
            fir.f_yetkiliTel = p1.f_yetkiliTel;

            db.SaveChanges();


            return RedirectToAction("UstaList");
        }


        // USTA GUNCELLE BİTİŞ





    }
}