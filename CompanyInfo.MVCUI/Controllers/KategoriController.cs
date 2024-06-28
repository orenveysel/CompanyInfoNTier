using AspNetCoreHero.ToastNotification.Abstractions;
using CompanyInfo.BL.Managers.Abstract;
using CompanyInfo.Entities.Models.Concrete;
using CompanyInfo.MVCUI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyInfo.MVCUI.Controllers
{
    public class KategoriController : Controller
    {
        private readonly IKategoriManager kategoriManager;
        private readonly IUrunManager urunManager;
        private readonly INotyfService notifyService;

        public KategoriController(IKategoriManager kategoriManager,
        IUrunManager urunManager,
        INotyfService notifyService)
        {
            this.kategoriManager = kategoriManager;
            this.urunManager = urunManager;
            this.notifyService = notifyService;
        }

        public async Task<IActionResult> Index()
        {
            var kategoriler = kategoriManager.GetAll();
            return View(kategoriler);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //Bu class da tanimlamis oldugumuz Data Annotation 'larin calismasi icin instance alinmasi gerekir.
            KategoriInsertVM insertVM = new KategoriInsertVM();

            return View(insertVM);

        }

        [HttpPost]
        public IActionResult Create(KategoriInsertVM insertVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {

                int sonuc = 0;
                Kategori kategori = new()
                {
                    KategoriAdi=insertVM.KategoriAdi,
                    Aciklama=insertVM.Aciklama
                };
                try
                {
                     sonuc = kategoriManager.Insert(kategori);
                
                }
                catch (Exception ex)
                {
                    string errorMesaj = "";
                    if (ex.InnerException.ToString().Contains("duplicate"))
                    {
                        errorMesaj = "Bu kategori daha onceden tanimlanmistir";
                    }
                    else
                    {
                        errorMesaj = "Beklenmedik bir hata  olustu:";
                    }
                    ModelState.AddModelError("", errorMesaj);
                    return View();
                }
               
                if (sonuc > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Beklenmedik bir hata olustu.");
                    return View();
                }
            }
        }

        public async Task<IActionResult> KategoriyeUrunEkle(int Id)
        {
            List<KategoriCheckBoxVM> kategoriChecks = new List<KategoriCheckBoxVM>();
            var kategori = await kategoriManager.GetAllInclude(p => p.Id == Id, p => p.Urunler).FirstOrDefaultAsync();
            ViewBag.Kategori = kategori;
            var urunler = urunManager.GetAll();
            foreach (var item in urunler)
            {
                KategoriCheckBoxVM vM = new()
                {
                    Id = item.Id,
                    IsChecked =false,
                    LabelName = item.UrunAdi,
                    KategoriId = Id
                };

                kategoriChecks.Add(vM);
            }
            foreach (var item in kategori.Urunler)
            {
                if (kategoriChecks.Any(p => p.Id == item.Id))
                {
                    var kat = kategoriChecks.Where(p => p.Id == item.Id).FirstOrDefault();
                    kat.IsChecked= true;

                }
            }

            return View(kategoriChecks);
        }

        [HttpPost]
        public async Task<IActionResult> KategoriyeUrunEkle(List<KategoriCheckBoxVM> checkBoxVMs)
        {

            List<Urun> silinenurunler = new List<Urun>();

            //1- Ilgili kategoriye ait urunleri database'den almak
            var kategoriId = checkBoxVMs.FirstOrDefault().KategoriId;
            var kategori = kategoriManager.GetAllInclude(p=>p.Id == kategoriId,p=>p.Urunler).FirstOrDefault();

            //2- gelen checkBoxVMs icerisinde uncheck edilan varmi ?

            foreach (var item in kategori.Urunler)
            {
                if (checkBoxVMs.Any(p => p.Id == item.Id && p.IsChecked == false))
                {
                    //var kategori = kategoriManager.GetById(item.Id);
                    silinenurunler.Add(item);

                }
            }

            //3- Eklenen urun varmi kontrolu
            foreach (var item in checkBoxVMs.Where(p => p.IsChecked == true))
            {
                if (!kategori.Urunler.Any(p => p.Id == item.Id))
                {
                    var urun = urunManager.GetById(item.Id);
                    kategori.Urunler.Add(urun);
                    
                }
            }
            //4- silinenler listesindeki urunleri kategoriden remove edilmesi 
            foreach (var item in silinenurunler)
            {
                kategori.Urunler.Remove(item);
            }
            int sonuc = kategoriManager.Update(kategori);
            if (sonuc > 0)
            {
                notifyService.Success("Urunler basarili sekilde kaydedildi");
                //notifyService.Error("Hata ..");
                //notifyService.Information("Info ...");
                //notifyService.Warning("Dikkat ...");
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
