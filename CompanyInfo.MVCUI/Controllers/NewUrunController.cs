using CompanyInfo.BL.Managers.Abstract;
using CompanyInfo.Entities.Models.Concrete;
using CompanyInfo.MVCUI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CompanyInfo.MVCUI.Controllers
{
    public class NewUrunController : Controller
    {
        private readonly IUrunManager urunManager;

        private readonly IBirimManager BirimManager;
        private readonly IKategoriManager kategoriManager;

        public NewUrunController(IUrunManager urunManager,
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

        public IActionResult KategoriYonet(int id)
        {
            if (id == 0 || id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                KategoriYonetVM kategoriYonet = new();
                kategoriYonet.Urun = urunManager.GetAllInclude(p => p.Id == id, p => p.Kategoriler).FirstOrDefault();
                kategoriYonet.Kategoriler = kategoriManager.GetAll();
               kategoriYonet.KategoriIdler = kategoriManager.GetAll().Select(k => k.Id).ToList();
               
                return View(kategoriYonet);
            }

        }

        [HttpPost]
        public IActionResult KategoriYonet(Urun urun, List<int> kategoriIdleri)
        {


            return View();
        }
        public IActionResult KategoriYonet2(int id)
        {
            List<CheckBoxVM> checkList = new();

            var kategoriListesi = kategoriManager.GetAll();
            

            var urun = urunManager.GetAllInclude(p => p.Id == id, p => p.Kategoriler).FirstOrDefault();

            foreach (var item in kategoriListesi)
            {
                var kategoriVarmi = urun.Kategoriler.Any(p => p.KategoriAdi == item.KategoriAdi);
                CheckBoxVM checkBox = new()
                {
                    Id = item.Id,
                    LabelName = item.KategoriAdi,
                    IsChecked = kategoriVarmi,
                    EntityId = id,
                   
                };
                checkList.Add(checkBox);
            }

           
            return View(checkList);
        }
        [HttpPost]
        public IActionResult KategoriYonet2(List<CheckBoxVM> checkBoxes)
        {

            var urun = urunManager.GetAllInclude(p=>p.Id==checkBoxes.First().EntityId,p=>p.Kategoriler).FirstOrDefault();
            foreach (var checkBox in checkBoxes.Where(p => p.IsChecked == true))
            {
                if (!urun.Kategoriler.Any(p => p.Id == checkBox.Id))
                {
                    var kategori = kategoriManager.GetById(checkBox.Id);
                    if (kategori != null)
                    {
                        urun.Kategoriler.Add(kategori);

                    }
                }
                
                
            }
            urunManager.Update(urun);
            return View();
        }
    }
}
