using Microsoft.AspNetCore.Mvc;

namespace CompanyInfo.WebMvc.Controllers
{
    public class IletisimController :Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
