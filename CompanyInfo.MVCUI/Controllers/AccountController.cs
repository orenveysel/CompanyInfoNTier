using CompanyInfo.BL.Managers.Abstract;
using CompanyInfo.Entities.Models.Concrete;
using CompanyInfo.MVCUI.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CompanyInfo.MVCUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IManager<User> userManager;

        public AccountController(IManager<User> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult Login()
        {
            LoginVM loginVM = new LoginVM();
            return View(loginVM);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
           if(!ModelState.IsValid) 
            return View(loginVM);

           var user = userManager.GetAllInclude(p=>p.Email==loginVM.Email && p.Password==loginVM.Password,p=>p.Roller).FirstOrDefault();
            if (user == null)
            {
                ModelState.AddModelError("", "Email yada şifre hatalidir");
                return View();
            }
            string roleStr = "";
            //foreach (var item in kullanici.Entity.Roles)
            //{
            //    roleStr = roleStr + item.RoleName + ",";
            //}
            //var roller = roleStr.Substring(0, roleStr.Length - 1);


            // Olusturulacak kimlik kartinin üzerindeki alanlar
            var claims = new List<Claim>()
            {
                
                new Claim(ClaimTypes.Email, loginVM.Email),
                new Claim(ClaimTypes.Role,user.Roller.FirstOrDefault().RoleAdi),
                new Claim(ClaimTypes.MobilePhone,user.Gsm),
                new Claim(ClaimTypes.Name,user.Ad + " " + user.Soyad),
                new Claim(ClaimTypes.Surname,user.Soyad),
                
            };

            //       public ClaimsPrincipal User { get; }

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authenticationProperty = new AuthenticationProperties
            {
                IsPersistent = loginVM.RememberMe


            };
            var userClaimPrincipal = new ClaimsPrincipal(claimIdentity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
               new ClaimsPrincipal(claimIdentity),
                authenticationProperty);

            return RedirectToAction("Index", "Home", new { Area = "Admin" });
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Yasak()
        {
            return View();
        }
    }
}
