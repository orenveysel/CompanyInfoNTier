using CompanyInfo.Entities.Models.Concrete;

namespace CompanyInfo.WebMvc.Models
{
    public class GenelListeVM
    {
        public List<Urun> Urunler { get; set; }
        public List<Kategori> Kategoriler { get; set; }
        public List<Tedarikci> Tedarikciler { get; set; }
    }
}
