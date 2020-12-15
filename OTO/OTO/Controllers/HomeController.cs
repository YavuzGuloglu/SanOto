using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OTO.Models.Entity;
namespace OTO.Controllers
{
    
    public class HomeController : Controller
    {
        SanOtoEntities db = new SanOtoEntities();
        public ActionResult Index()
        {
           
            var a = db.Kullanici.Count();
            ViewBag.deger1 = a;
            var b = db.Ilan.Count();
            ViewBag.deger2 = b;
            var c = db.Firma.Count();
            ViewBag.deger3 = c;

            return View();
        }
   
        public ActionResult Ilanlar()
        {
            var ilan = db.Ilan.ToList();

            return View(ilan);
        }
      
        public ActionResult IlanDetay(int id)
        {
            var ilan = db.Ilan.Find(id);

            return View("IlanDetay",ilan);
        }
        public ActionResult UstaDetay(int id)
        {
            var firma = db.Firma.Find(id);

            return View("UstaDetay", firma);
        }

        public ActionResult Ustalar()
        {
            var usta = db.Firma.ToList();
            return View(usta);
        }

        public ActionResult Iletisim()
        {
            return View();
        }
    }
}