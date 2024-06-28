using CompanyInfo.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyInfo.WebMvc.Controllers
{
    public class HomeController:Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult PersonList()
        {

            List<Person> persons = new List<Person>()
            {
                new Person{ Id=1,AdSoyad="Ali ",Email="Ali@gmail.com",Gsm="123123",Sehir="Mus"},
                 new Person{ Id=2,AdSoyad="veli ",Email="Ali@gmail.com",Gsm="123123",Sehir="van"},
                  new Person{ Id=3,AdSoyad="ayse ",Email="Ali@gmail.com",Gsm="123123",Sehir="Adana"},
                   new Person{ Id=4,AdSoyad="fatma ",Email="Ali@gmail.com",Gsm="123123",Sehir="Ankara"}
            };
            return View(persons);
        }
    }
}
