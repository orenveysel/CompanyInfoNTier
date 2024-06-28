using CompanyInfo.Entities.Models.Abstract;

namespace CompanyInfo.Entities.Models.Concrete
{
    public class Tedarikci :BaseEntity
    {

        public string SirketAdi { get; set; }
        public string VergiNo { get; set; }

        public ICollection<Urun> Urunler { get; set; }
    }
}