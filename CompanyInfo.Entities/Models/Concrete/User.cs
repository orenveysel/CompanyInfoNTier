using CompanyInfo.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CompanyInfo.Entities.Models.Concrete
{
    public class User:BaseEntity
    {
      
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Gsm { get; set; }
        public string Email { get; set; }
        public string Password{ get; set; }
        public bool? Cinsiyet { get; set; }
        public string? ImagePath { get; set; }
        public ICollection<Role> Roller { get; set; } = new List<Role>();
    }
}
