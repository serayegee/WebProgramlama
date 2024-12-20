﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using odevweb.Models;

namespace odevweb.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
  

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdminPanel() 
        {
            return View();
        }

        public IActionResult KullaniciListesi() 
        { 
            using (var context = new KuaforContext())
            {
                var kullanicilar = context.Kullanicis.ToList();
                return View(kullanicilar);
            }
        }

        [HttpGet]
        public IActionResult KullaniciEkle()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult KullaniciEkle(Kullanici model)
        {
            if(ModelState.IsValid)
            {
                using (var context = new KuaforContext()) 
                { 
                    context.Kullanicis.Add(model);
                    context.SaveChanges();
                }
                return RedirectToAction("KullaniciListesi");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult KullaniciDuzenle(int id)
        {
            using(var context = new KuaforContext()) 
            {
                var kullanici = context.Kullanicis.FirstOrDefault(k => k.KullaniciId == id);
                if(kullanici == null) { return NotFound(); }
                return View(kullanici);

            }
        }

        [HttpPost]
        public IActionResult KullaniciDuzenle(Kullanici model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new KuaforContext())
                {
                    var kullanici = context.Kullanicis.FirstOrDefault(k => k.KullaniciId == model.KullaniciId);
                    if (kullanici != null)
                    {
                        kullanici.Ad = model.Ad;
                        kullanici.KullaniciAdi = model.KullaniciAdi;
                        kullanici.Sifre = model.Sifre; // Şifreyi hashlemek gerekebilir
                        kullanici.IsAdmin = model.IsAdmin;

                        context.SaveChanges();
                        return RedirectToAction("KullaniciListesi");
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult KullaniciSil(int id)
        {
            using (var context = new KuaforContext())
            {
                var kullanici = context.Kullanicis.FirstOrDefault(k => k.KullaniciId == id);
                if (kullanici == null)
                {
                    return NotFound(); // Kullanıcı bulunamazsa 404 döndür
                }
                return View(kullanici); // Silmeden önce kullanıcıyı göster
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult KullaniciSilConfirmed(int id)
        {
            if (id == 0) // id'nin 0 veya null olup olmadığını kontrol edin
            {
                return NotFound("Geçersiz Kullanıcı ID");
            }

            using (var context = new KuaforContext())
            {
                var kullanici = context.Kullanicis.FirstOrDefault(k => k.KullaniciId == id);
                if (kullanici != null)
                {
                    context.Kullanicis.Remove(kullanici);
                    context.SaveChanges();
                }

                return RedirectToAction("KullaniciListesi");
            }
        }
        
        [HttpGet]
        public IActionResult PersonelEkle()
        {
           /*
             using (var context = new KuaforContext())
             {
                 // İşlem listesini dropdown için SelectList olarak ViewBag'e gönderiyoruz
                 ViewBag.IslemId = new SelectList(context.Islems, "IslemId", "Ad");
                 return View();
             }*/
            /*
            using (var context = new KuaforContext())
            {
                var model = new Personel
                {
                    personel = new Personel(),
                    Islemler = context.Islems.ToList() // Dropdown için işlemleri al
                };
                return View(model);
            }*/
             using (var context = new KuaforContext())
             {
                // İşlem listesini dropdown için ViewBag ile gönderiyoruz
                ViewBag.Islemler = context.Islems.ToList();
                 return View();
             }

        }

        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PersonelEkle(Personel model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new KuaforContext())
                {
                    // Girilen IslemId'nin veritabanında mevcut olup olmadığını kontrol edin
                    var islem = context.Islems.FirstOrDefault(i => i.IslemId == model.IslemId);
                    if (islem == null)
                    {
                        ModelState.AddModelError("IslemId", "Geçersiz İşlem ID. Lütfen doğru bir ID girin.");
                        return View(model);
                    }

                    // Personel kaydını ekle
                    context.Personels.Add(model);
                    context.SaveChanges();

                    return RedirectToAction("PersonelListesi");
                }
            }

            // Hata durumunda aynı sayfayı döndür
            return View(model);
        }
        */

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PersonelEkle(Personel model)
        {
          
            
            if (ModelState.IsValid)
            {
                using (var context = new KuaforContext())
                {
                    context.Personels.Add(model);
                    context.SaveChanges();
                    return RedirectToAction("PersonelListesi");
                }
            }

            // Hata durumunda dropdown'u doldur
            using (var context = new KuaforContext())
            {
                ViewBag.Islemler = context.Islems.ToList();
            }
            return View(model);
        }


        public IActionResult IslemListesi()
        {
            using (var context = new KuaforContext())
            {
                var islemler = context.Islems.ToList(); // Veritabanından işlemleri çek
                return View(islemler); // Görünümü işlemlerle birlikte döndür
            }
        }

        public IActionResult PersonelListesi()
        {
            using (var context = new KuaforContext())
            {
                var personeller = context.Personels.ToList(); // Veritabanından işlemleri çek
                return View(personeller); // Görünümü işlemlerle birlikte döndür
            }
        }

    }
}
