using CompanyInfo.BL.Managers.Abstract;
using CompanyInfo.BL.Managers.Concrete;
using CompanyInfo.Entities.Models.Concrete;
using CompanyInfo.MVCUI.Areas.Admin.Models;
using CompanyInfo.MVCUI.Models;
using CompanyInfo.MVCUI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyInfo.MVCUI.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        private readonly IManager<User> userManager;
        private readonly IManager<Role> roleManager;
        private readonly IHostEnvironment hostingEnvironment;

        public UserController(IManager<User> userManager,
            IManager<Role> roleManager,
            IHostEnvironment hostingEnvironment)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            var users = userManager.GetAllInclude(null, p => p.Roller).ToList();
            return View(users);
        }

        public IActionResult Create()
        {
            UserInsertVM insertVM = new UserInsertVM();
            return View(insertVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserInsertVM vM)
        {
            //TODO  
            if (vM.UserImage!= null) 
            {
                if (vM.UserImage.Length > 0)
                {

                    #region wwwroot icerisinde uploads klasorune gelen dosyayi kaydetmek icin
                    //Getting FileName
                    var fileName = Path.GetFileName(vM.UserImage.FileName);
                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);
                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
                    string uploads = Path.Combine(hostingEnvironment.ContentRootPath, $@"wwwroot/images/{newFileName}");
                    string filePath = Path.Combine(uploads, newFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await vM.UserImage.CopyToAsync(fileStream);
                    }
                    #endregion
                  
                    var user = new User
                    {
                        Ad=vM.Ad,
                        Soyad=vM.Soyad,
                        Cinsiyet=vM.Cinsiyet,
                        Email=vM.Email,
                        Gsm = vM.Gsm==null?"Belirtilmemis": vM.Gsm,
                        Password=vM.Password,
                        ImagePath = filePath
                        

                    };
                   
                    #region Database 'e kaydetmek icin
                     var role = roleManager.GetById(2);
                    userManager.Insert(user);
                    user.Roller.Add(role);
                    userManager.Update(user);
                    
                    #endregion

                   
                }
            }
            return View();
        }

        public async Task<IActionResult> Edit(int userId)
        {
            UserUpdateVM updateVM = new UserUpdateVM();


            List<CheckBoxVM> checkBoxes = new();
            var user = userManager.GetAllInclude(p=>p.Id==userId ,p=>p.Roller).FirstOrDefault();

            updateVM.MyUser = user;
            // Butun Roller Database'den Getiriliyor
            var roller = roleManager.GetAll();

            // roller checkboxes listesine atiliyor
            foreach (var item in roller)
            {
                CheckBoxVM checkBox = new()
                {
                    Id = item.Id,
                    IsChecked = false,
                    LabelName = item.RoleAdi,
                    EntityId = user.Id

                };
                checkBoxes.Add(checkBox);
            }
            //bu user'a ait herhangi bir rol var ise checkboxes icerisndeki elemanin 
            // ischeck property'si true yapiliyor
            foreach (var item in user.Roller)
            {
                if (checkBoxes.Any(p => p.Id == item.Id))
                {
                    var checkbox = checkBoxes.Where(p => p.Id == item.Id).FirstOrDefault();
                    checkbox.IsChecked = true;
                }
            }
            updateVM.Roller = checkBoxes;
            return View(updateVM);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateVM updateVM)
        {

            List<Role> silinenler = new();

            var userId = updateVM.MyUser.Id;
            //Database'den ilgili kayda ulasiyoruz

            var user = userManager.GetAllInclude(p=>p.Id==userId,p=>p.Roller).FirstOrDefault();


            if (!ModelState.IsValid)
            {
                return View();
            }

            //2- gelen checkBoxVMs icerisinde uncheck edilan varmi ?

            foreach (var item in user.Roller)
            {
                if (updateVM.Roller.Any(p => p.Id == item.Id && p.IsChecked == false))
                {
                    silinenler.Add(item);
                }
            }

            //3- Eklenen role varmi kontrolu
            foreach (var item in updateVM.Roller.Where(p => p.IsChecked == true))
            {
                if (!user.Roller.Any(p => p.Id == item.Id))
                {
                    var role = roleManager.GetById(item.Id);
                    user.Roller.Add(role);

                }
            }
            //4- silinenler listesindeki rolleri user'dan  remove edilmesi 
            foreach (var item in silinenler)
            {
               user.Roller.Remove(item);
            }
            int sonuc = userManager.Update(user);
            if (sonuc > 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
