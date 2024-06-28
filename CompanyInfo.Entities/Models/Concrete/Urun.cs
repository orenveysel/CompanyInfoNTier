using CompanyInfo.Entities.Models.Abstract;

namespace CompanyInfo.Entities.Models.Concrete
{
    public class Urun:BaseEntity
    {
        public string UrunAdi { get;  set; }
        public string? UrunKodu { get; set; }
        public double? Fiyat { get;  set; }

        public double? Adet { get; set; }

        public bool NegatifStokCalis { get; set; }
        #region Birim ile 1-N iliski tanimlamasi
        public int BirimId { get; set; }
        public Birim Birim { get; set; } 
        #endregion

        //Kategoriler ile N-N iliski tanimlamasi
        public ICollection<Kategori> Kategoriler { get; set; }= new List<Kategori>();

        #region Tedarikci 

        #region ile 1-N Tanimlamasi
        //public int? TedarikciId { get; set; }
        //public Tedarikci? Tedarikci { get; set; } 
        #endregion

        #region N-N Tanimlamasi
        public ICollection<Tedarikci> Tedarikciler { get; set; }
        #endregion


        #region Fotograflari icin 1-N iliski

        public ICollection<UrunFoto>? Fotograflar { get; set; }

        #endregion
        #endregion
    }
}