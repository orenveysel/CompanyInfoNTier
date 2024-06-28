using System.ComponentModel.DataAnnotations;

namespace CompanyInfo.MVCUI.Models.ViewModels
{
    public class TedarikciInsertVM
    {

        [Required(ErrorMessage ="Sirket Adi Zorunludur")]
        [MaxLength(50,ErrorMessage ="50 karakterden fazla olamaz")]
        [MinLength(2,ErrorMessage ="En Az 2 karakter olmalidir")]
        public string SirketAdi { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Vergi No Zorunludur")]
        [MaxLength(20, ErrorMessage = "20 karakterden fazla olamaz")]
        [MinLength(2, ErrorMessage = "' karakter ve uzeri olmak zorundadir")]
        public string VergiNo { get; set; }
    }
}
