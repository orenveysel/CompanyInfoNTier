using CompanyInfo.BL.Managers.Abstract;
using CompanyInfo.Entities.Models.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyInfo.MVCUI.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BirimController : Controller
    {
        private readonly IBirimManager birimManager;

        public BirimController(IBirimManager birimManager)
        {
            this.birimManager = birimManager;
        }
        public IActionResult Index()
        {
            var birimler = birimManager.GetAll();
            return View(birimler);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var birim = new Birim();
            return View(birim);
        }

        [HttpPost]
        public IActionResult Create(Birim birim)
        { 
            if (ModelState.IsValid)
            {
                birimManager.Insert(birim);
                return RedirectToAction(nameof(Index));
            }

           
            return View();
        }
        public IActionResult Update(int id)
        {
            return View();
        }
        public IActionResult Delete(int id)
        {
          
            birimManager.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
