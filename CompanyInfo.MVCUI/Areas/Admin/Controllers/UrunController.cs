using CompanyInfo.BL.Managers.Abstract;
using CompanyInfo.Entities.Models.Concrete;
using CompanyInfo.MVCUI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyInfo.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UrunController : Controller
    {
        private readonly IUrunManager urunManager;

        private readonly IBirimManager BirimManager;
        private readonly IKategoriManager kategoriManager;

        public UrunController(IUrunManager urunManager,
            IBirimManager birimManager,
            IKategoriManager kategoriManager)
        {
            this.urunManager = urunManager;
            BirimManager = birimManager;
            this.kategoriManager = kategoriManager;
        }

        public IActionResult Index()
        {
            var urunler = urunManager.GetAllInclude(null, p => p.Birim);
            return View(urunler);

        }

        public IActionResult Create()
        {
            //Kendi Yazdigimiz validation'larin oldugu Class'tan 
            //Instance aliyoruz ve bunu view'e gonderiyoruz
            UrunInsertVM insertVM = new UrunInsertVM();
            ViewBag.birimler = BirimManager.GetAll();
            return View(insertVM);
        }
        // Olusturulan form'un bizim tarafimizdan olusturuldugunu garanti eder

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(UrunInsertVM insertVM)
        {
            //Gelen Model dogru gelmişmi ? 
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Birşeyler yanliş gitti");
                ViewBag.birimler = BirimManager.GetAll();
                return View();
            }
            else
            {
                //Amele yontemi ile
                Urun yeniUrun = new Urun()
                {
                    UrunKodu = insertVM.UrunKodu,
                    UrunAdi = insertVM.UrunAdi,
                    BirimId = insertVM.BirimId,
                    Adet = insertVM.Adet,
                    Fiyat = insertVM.Fiyat
                };
                var sonuc = urunManager.Insert(yeniUrun);
                if (sonuc > 0)
                    return RedirectToAction("Index");
                else
                {

                    ModelState.AddModelError("", "Beklenmedik bir hata olustu. Lütfen daha sonra tekrar deneyiniz");
                    return View();
                }

            }

        }

        [HttpPost]
        public IActionResult KategoriYonetYeni(List<CheckBoxVM> checkBoxes)
        {

            //Burada urunId'si gelen liste icerisinde alinir.
            var urunId = checkBoxes.First().EntityId;
            // Gelen urun id ile bu urunu kategorileri ile database'den istedik
            var urun = urunManager.GetAllInclude(p => p.Id == urunId, p => p.Kategoriler).FirstOrDefault();

            // Burada silinecek kategoriler listesi olusturduk
            List<Kategori> silinenler = new List<Kategori>();

            //database'den gelen urun icerisindeki kategorilerden hangisi
            // unchecked edilmi onu bulup silinenler listesine atiyoruz
            foreach (var item in urun.Kategoriler)
            {
                if (checkBoxes.Any(p => p.Id == item.Id && p.IsChecked == false))
                {
                    //var kategori = kategoriManager.GetById(item.Id);
                    silinenler.Add(item);

                }
            }

            //Burada da Check edilenlerin icerisinde olup ddatabase'den gelen 
            //urunun kategorilerinde olmayan kategoriyi bulup ekliyoruz
            foreach (var item in checkBoxes.Where(p => p.IsChecked == true))
            {
                if (!urun.Kategoriler.Any(p => p.Id == item.Id))
                {
                    var kategori = kategoriManager.GetById(item.Id);
                    urun.Kategoriler.Add(kategori);
                }
            }

            //Olusturulan silinenler listesindekileri remode ediyoruz
            foreach (var item in silinenler)
            {
                urun.Kategoriler.Remove(item);
            }

            //urun.Kategoriler.Clear();
            //urunManager.Update(urun);

            //foreach (var item in checkBoxes.Where(p=>p.IsChecked==true))
            //{
            //    var kategori = kategoriManager.GetById(item.Id);
            //    if (kategori != null)
            //    {
            //        urun.Kategoriler.Add(kategori);
            //    }

            //}
            //Manager Sinifina update islemi yaptiriyoruz
            var sonuc = urunManager.Update(urun);
            if (sonuc > 0) return RedirectToAction("Index");
            else
                return View(checkBoxes);
        }
        public IActionResult KategoriYonetYeni(int id)
        {
            List<CheckBoxVM> checkedList = new List<CheckBoxVM>();

            var kategoriler = kategoriManager.GetAll();
            var urun = urunManager.GetAllInclude(p => p.Id == id, x => x.Kategoriler).FirstOrDefault();
            ViewBag.Urun = urun;

            foreach (var kategori in kategoriler)
            {
                var check = new CheckBoxVM()
                {
                    Id = kategori.Id,
                    LabelName = kategori.KategoriAdi,
                    EntityId = id,
                    IsChecked = urun.Kategoriler.Any(p => p.Id == kategori.Id)
                };
                checkedList.Add(check);
            }
            return View(checkedList);
        }
        public IActionResult KategoriYonet(int id)
        {
            ViewBag.Kategoriler = kategoriManager.GetAll();
            var urun = urunManager.GetAllInclude(p => p.Id == id, p => p.Kategoriler).FirstOrDefault();
            return View(urun);


        }

        [HttpPost]
        public IActionResult KategoriYonet(Urun urun, List<int> kategoriIdleri)
        {


            return View();
        }


    }
}
