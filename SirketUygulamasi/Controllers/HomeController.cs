using SirketUygulamasi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace SirketUygulamasi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        #region Şirket İşlemleri
        public ActionResult SirketEkle()
        {
            ViewBag.Message = "Şirket Ekleme";

            return View();
        }
        [HttpPost]
        public ActionResult SirketEkle(Sirket model)
        {
            using (var context = new CrudContext())
            {
                context.Sirketler.Add(model);
                context.SaveChanges();
            }
            return View();
        }


        [HttpGet]
        public ActionResult SirketListe()
        {
            ViewBag.Message = "Şirket Liste";
            using (var context = new CrudContext())
            {
                var data = context.Sirketler.ToList();
                return View(data);
            }
        }

        [HttpGet]
        public ActionResult SirketSil(int id)
        {
            using (var context = new CrudContext())
            {
                var data = context.Sirketler.FirstOrDefault(x => x.SIRKET_ID == id);
                if (data != null)
                {
                    context.Sirketler.Remove(data);
                    context.SaveChanges();
                }
                return RedirectToAction("SirketListe");
            }
        }


        [HttpGet]
        public ActionResult SirketDuzenle(int id)
        {
            using (var context = new CrudContext())
            {
                var data = context.Sirketler.Where(x => x.SIRKET_ID == id).SingleOrDefault();
                return View(data);
            }
        }
        [HttpPost]
        public ActionResult SirketDuzenle(Sirket Model)
        {
            using (var context = new CrudContext())
            {
                var data = context.Sirketler.Where(x => x.SIRKET_ID == Model.SIRKET_ID).FirstOrDefault();
                if (data != null)
                {
                    data.SIRKET_ADI = Model.SIRKET_ADI;
                    data.VERGI_DAIRESI = Model.VERGI_DAIRESI;
                    data.VERGI_NUMARASI = Model.VERGI_NUMARASI;
                    data.ADRES = Model.ADRES;
                    context.SaveChanges();
                }

                return RedirectToAction("SirketListe");
            }
        }


        #endregion

        #region Çalışan İşlemleri

        public ActionResult CalisanEkle()
        {
            ViewBag.Message = "Çalışan Ekleme";

            using (var context = new CrudContext())
            {
                var data = context.Sirketler.ToList();
                ViewBag.vb_SIRKETLER = new SelectList(data, "SIRKET_ID", "SIRKET_ADI");
            }

            return View();

        }
        [HttpPost]
        public ActionResult CalisanEkle(Calisan model)
        {

            using (var context = new CrudContext())
            {
                var data = context.Sirketler.ToList();
                ViewBag.vb_SIRKETLER = new SelectList(data, "SIRKET_ID", "SIRKET_ADI");

                context.Calisanlar.Add(model);
                context.SaveChanges();

            }
            return View();
        }


        [HttpGet]
        public ActionResult CalisanListe()
        {
            ViewBag.Message = "Çalışan Liste";
            using (var context = new CrudContext())
            {
                var data = context.Calisanlar.ToList();
                var newDtoList = new List<DTO_CALISAN>();
                foreach (var item in data)
                {
                    var newDto = new DTO_CALISAN();
                    var sirketAd = context.Sirketler.Where(x => x.SIRKET_ID == item.CALISAN_SIRKET_ID).Select(x => x.SIRKET_ADI).FirstOrDefault();
                    newDto.CALISAN_ID = item.CALISAN_ID;
                    newDto.ADI_SOYADI = item.ADI_SOYADI;
                    newDto.TC_NO = item.TC_NO;
                    newDto.CALISAN_SIRKET_ID = item.CALISAN_SIRKET_ID;
                    newDto.SIRKET_ADI = sirketAd;
                    newDtoList.Add(newDto);
                }

                return View(newDtoList);
            }
        }

        [HttpGet]
        public ActionResult CalisanSil(int id)
        {
            using (var context = new CrudContext())
            {
                var data = context.Calisanlar.FirstOrDefault(x => x.CALISAN_ID == id);
                if (data != null)
                {
                    context.Calisanlar.Remove(data);
                    context.SaveChanges();
                }
                return RedirectToAction("CalisanListe");
            }
        }


        [HttpGet]
        public ActionResult CalisanDuzenle(int id)
        {
            using (var context = new CrudContext())
            {
                var sirketData = context.Sirketler.ToList();
                ViewBag.vb_SIRKETLER = new SelectList(sirketData, "SIRKET_ID", "SIRKET_ADI");

                var data = context.Calisanlar.Where(x => x.CALISAN_ID == id).SingleOrDefault();
                return View(data);
            }
        }
        [HttpPost]
        public ActionResult CalisanDuzenle(Calisan Model)
        {
            using (var context = new CrudContext())
            {

                var data = context.Calisanlar.Where(x => x.CALISAN_ID == Model.CALISAN_ID).FirstOrDefault();
                if (data != null)
                {
                    data.ADI_SOYADI = Model.ADI_SOYADI;
                    data.TC_NO = Model.TC_NO;
                    data.CALISAN_SIRKET_ID = Model.CALISAN_SIRKET_ID;
                    context.SaveChanges();
                }

                return RedirectToAction("CalisanListe");
            }
        }
        #endregion

    }
}