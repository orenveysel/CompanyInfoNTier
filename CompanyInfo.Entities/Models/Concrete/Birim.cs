using CompanyInfo.Entities.Models.Abstract;
using System.Net.Http.Headers;
using System.Security.Principal;

namespace CompanyInfo.Entities.Models.Concrete
{
    public class Birim:BaseEntity
    {
        public string BirimAdi { get; set; }

        public ICollection<Urun>? Urunler { get; set; }
    }
}