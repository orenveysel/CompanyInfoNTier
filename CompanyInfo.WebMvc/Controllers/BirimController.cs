using CompanyInfo.BL.Managers.Abstract;
using CompanyInfo.BL.Managers.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CompanyInfo.WebMvc.Controllers
{
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
    }
}
