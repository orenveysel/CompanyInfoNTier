using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using CompanyInfo.BL.Managers.Abstract;
using CompanyInfo.BL.Managers.Concrete;
using CompanyInfo.Entities.Models.Concrete;
using CompanyInfo.MVCUI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyInfo.MVCUI.Controllers
{
    public class TedarikciController : Controller
    {
        private readonly IManager<Tedarikci> tedarikciManager;
        private readonly IUrunManager urunManager;
        private readonly INotyfService notifyService;

        public TedarikciController(IManager<Tedarikci> tedarikciManager,IUrunManager urunManager,INotyfService notifyService)
        {
            this.tedarikciManager = tedarikciManager;
            this.urunManager = urunManager;
            this.notifyService = notifyService;
        }
        public IActionResult Index()
        {
            var result = tedarikciManager.GetAll();
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            TedarikciInsertVM vM = new();

            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TedarikciInsertVM insertVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Tedarikci tedarikci = new()
            {
                SirketAdi = insertVM.SirketAdi,
                VergiNo = insertVM.VergiNo,
                CreateDate = DateTime.Now
            };
          int sonuc=  tedarikciManager.Insert(tedarikci);
            if (sonuc > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }




        public async Task<IActionResult> TedarikciyeUrunEkle(int TedarikciId)
        {
            List<TedarikciCheckBoxVM> tedarikciChecks = new List<TedarikciCheckBoxVM>();
            
            //Tedarikci bilgisini urunleri ile beraber databaseDen cekiyoruz
            var tedarikci = await tedarikciManager.GetAllInclude(p => p.Id == TedarikciId, p => p.Urunler).FirstOrDefaultAsync();
            ViewBag.Tedarikci = tedarikci;
            var urunler = urunManager.GetAll();
            foreach (var item in urunler)
            {
                TedarikciCheckBoxVM vM = new()
                {
                    Id = item.Id,
                    IsChecked = false,
                    LabelName = item.UrunAdi,
                    TedarikciId = TedarikciId
                };

                tedarikciChecks.Add(vM);
            }

            //Database'den cektigim tedarikcinin urunlerini kontrol ediyoruz.
            // Eger tedarikciChecks listesinde varsa  IsChecked alanini true olarak isaretliyoruz
            foreach (var item in tedarikci.Urunler)
            {
                if (tedarikciChecks.Any(p => p.Id == item.Id))
                {
                    var kat = tedarikciChecks.Where(p => p.Id == item.Id).FirstOrDefault();
                    kat.IsChecked = true;

                }
            }

            return View(tedarikciChecks);
        }

        [HttpPost]
        public async Task<IActionResult> TedarikciyeUrunEkle(List<TedarikciCheckBoxVM> checkBoxVMs)
        {

            List<Urun> silinenurunler = new List<Urun>();

            //1- Ilgili kategoriye ait urunleri database'den almak
            var tedarikciId = checkBoxVMs.FirstOrDefault().TedarikciId;
            var tedarikci = tedarikciManager.GetAllInclude(p => p.Id == tedarikciId, p => p.Urunler).FirstOrDefault();

            //2- gelen checkBoxVMs icerisinde uncheck edilan varmi ?

            foreach (var item in tedarikci.Urunler)
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
                if (!tedarikci.Urunler.Any(p => p.Id == item.Id))
                {
                    var urun = urunManager.GetById(item.Id);
                    tedarikci.Urunler.Add(urun);

                }
            }
            //4- silinenler listesindeki urunleri kategoriden remove edilmesi 
            foreach (var item in silinenurunler)
            {
                tedarikci.Urunler.Remove(item);
            }
            int sonuc = tedarikciManager.Update(tedarikci);
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
