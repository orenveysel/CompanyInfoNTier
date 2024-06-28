using CompanyInfo.Entities.Models.Concrete;
using CompanyInfo.MVCUI.Models.ViewModels;

namespace CompanyInfo.MVCUI.Areas.Admin.Models
{
    public class UserUpdateVM
    {
        public User MyUser { get; set; }
        public List<CheckBoxVM> Roller { get; set; }

        public IFormFile? MyFile { get; set; }
    }
}
