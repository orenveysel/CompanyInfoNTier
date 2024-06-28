using CompanyInfo.Entities.Models.Concrete;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CompanyInfo.MVCUI.Models.ViewModels
{
    public class UrunInsertVM
    {

        [MaxLength(50,ErrorMessage ="Urun Adi 50 karakterden fazla olamaz")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Urun Adi Boş Geçilemez")]
        [DisplayName("Urun Adi:")]
        public string UrunAdi { get; set; }

        [MaxLength(50, ErrorMessage = "UrunKodu 50 karakterden fazla olamaz")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Urun Kodu Boş Geçilemez")]
        [DisplayName("Urun Kodu:")]
        public string? UrunKodu { get; set; }

        [DataType(DataType.Currency)]
        public double? Fiyat { get; set; }

        [DataType(DataType.Currency)]

        public double? Adet { get; set; }

        [DisplayName("Negatif Stok Çaliş sin mi:")]
        public bool NegatifStokCalis { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Birim zorunlu alandir")]
        [Display(Name ="Birim")]
        public int BirimId { get; set; }
        
    }
}
