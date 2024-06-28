using CompanyInfo.BL.Managers.Abstract;
using CompanyInfo.BL.Managers.Concrete;

namespace CompanyInfo.WebMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();// Mvc projesi icin gerekli servisleri yukler
            
            #region Manager siniflarinin register edilmesi
            //Solid prensiplerinden Dipendency Invertion prensibine istinaden ,Servislere Ilgili manager sinifinin Interface 'i uzerinden register edilmistir
            builder.Services.AddScoped<IBirimManager, BirimManager>();
            builder.Services.AddScoped<IUrunManager, UrunManager>();
            builder.Services.AddScoped<IKategoriManager, KategoriManager>();



            #endregion



            var app = builder.Build();


            app.UseStaticFiles();//Burasi wwwroot klasorunu web'e acar

            app.UseRouting(); // Cozumleme yapmasi icin gerekli metod. 

            //app.MapGet("/", () => "Hello World!");
            app.MapControllerRoute(
               name: "default",
               pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}
