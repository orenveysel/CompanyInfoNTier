using CompanyInfo.Entities.Models.Concrete;

namespace CompanyInfo.MVCUI.Models.ViewModels
{
    public class KategoriYonetVM
    {
        public Urun  Urun { get; set; }
        // public List<Kategori> Kategoriler { get; set; }
        public List<Kategori>? Kategoriler { get; set; }
        public List<int>? KategoriIdler { get; set; } = new List<int>();
    }
}
