using CompanyInfo.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyInfo.WebMvc.Controllers
{
    public class TestController :Controller
    {
        public IActionResult Index()
        {
            GenelListeVM genelListeVM = new GenelListeVM();
            return View(genelListeVM);
        }

        public IActionResult Hakkimizda()
        {
            return View();
        }
    }
}
