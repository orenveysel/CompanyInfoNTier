using CompanyInfo.Entities.Models.Abstract;

namespace CompanyInfo.Entities.Models.Concrete
{
    public class Role :BaseEntity
    {
        public string RoleAdi { get; set; }
        public ICollection<User> Kullanicilar { get; set; }
    }
}