using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyInfo.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize] // Class seviyesinde yetkilendirme demektir
   
    public class HomeController : Controller
    {

       // [AllowAnonymous]  //Burasi herkese acik demektir
        public IActionResult Index()
        {

            var result = HttpContext.User.Claims;
            return View();
        }
        [Authorize("Admin")] // Metod seviyesinde yetkilendirme 
        public IActionResult Test()
        {
            return View();
        }
        [Authorize("Admin,Satis")]
        public IActionResult Deneme()
        {
            return View();
        }
    }
}
