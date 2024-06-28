using CompanyInfo.BL.Managers.Abstract;
using CompanyInfo.Entities.Models.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CompanyInfo.MVCUI.Components
{
    public class MenuViewComponent:ViewComponent
    {
        private readonly IManager<Menu> menuManager;

        public MenuViewComponent(IManager<Menu> menuManager)
        {
            this.menuManager = menuManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var parentMenuler = menuManager.GetAll();
                
            return View(parentMenuler);
        }
    }
}
