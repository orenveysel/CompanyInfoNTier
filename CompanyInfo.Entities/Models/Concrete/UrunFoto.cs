using CompanyInfo.Entities.Models.Abstract;
using System.ComponentModel.DataAnnotations;

namespace CompanyInfo.Entities.Models.Concrete
{
    public class UrunFoto:BaseEntity
    {
       

        public string Name { get; set; }

        public string? FilePath { get; set; }

        public string? FileType { get; set; }
        public byte[] DataFiles { get; set; }
        public int UrunId { get; set; }
        public Urun Urun { get; set; }
      
       
        
       



    }
}