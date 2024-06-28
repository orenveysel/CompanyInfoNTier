using System.ComponentModel.DataAnnotations;

namespace CompanyInfo.MVCUI.Models.ViewModels
{
    public class KategoriInsertVM
    {

        [Required(AllowEmptyStrings =false,ErrorMessage ="Kategori Adi Zorunludur")]
        [StringLength(50,ErrorMessage ="En Fazla 50 karakter uzunlugunda olabilir")]
        [Display(Name ="Kategori Adi Giriniz:")]
        public string KategoriAdi { get; set; }

        [StringLength(500,ErrorMessage ="En Fazla 500 Karakter olabilir")]
        public string? Aciklama { get; set; }
    }
}
