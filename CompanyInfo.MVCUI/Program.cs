using CompanyInfo.BL.Managers.Abstract;
using CompanyInfo.BL.Managers.Concrete;
using CompanyInfo.Entities.DbContexts;
using CompanyInfo.Entities.Models.Abstract;
using Microsoft.EntityFrameworkCore;
using CompanyInfo.MVCUI.Extensions;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
namespace CompanyInfo.MVCUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string test = "";
            test.TurkceKArakterTemizle();

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
            //--------Eger Development Asamasinda ise AddRuntimeComplitaion 
            //-- Yani Runtime sirasinda Razor sayfalarindaki degisiklikleri yansitmaya
            //-- Yarar
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddControllersWithViews()
                    .AddRazorRuntimeCompilation();
                // Burasi Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation paketini
                // indirdikten sonra ancak çalistirilabilir.
            }
            else
            {
                builder.Services.AddControllersWithViews();
            }
                

            //----------Burasi Extension Metod haline Getirildi-----
            //----------Extension Klasoru icerisinde CompanyInfoService.cs dosyasindadir
            builder.Services.AddCompanyInfoServices();
            builder.Services.AddCookieAyarlar(); // CookieBase Authentication icin gerekli ayarlari ekler. Bu metod Extensions klasoru icerisinde ki class da mevcuttur

            //---------ToastNotification Ayari---------
            builder.Services.AddNotyf(config => 
            { config.DurationInSeconds = 10; 
                config.IsDismissable = true; 
                config.Position = NotyfPosition.BottomRight; 
            });

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("default"))
            );
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseNotyf();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
                );

            app.Run();
        }
    }
}
