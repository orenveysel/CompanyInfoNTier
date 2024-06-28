using CompanyInfo.BL.Managers.Abstract;
using CompanyInfo.Entities.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyInfo.BL.Managers.Concrete
{
    public class UrunManager : ManagerBase<Urun>, IUrunManager
    {
        public ICollection<Urun> KritikStokSeviyesiAltindakiler()
        {
           
            return base.GetAll(p=>p.Adet<10);
        }
        public override int Insert(Urun input)
        {
            #region Urun Adi Boşmu Kontrolu

            if (string.IsNullOrEmpty(input.UrunAdi))
            {
                throw new Exception("Urun Adi Boş Olamaz ");
            }

            if (input.UrunAdi.Trim().Length < 2)
            {
                throw new Exception("Urun Adi en Az 2 karekter olmalidir ");
            }
            #endregion

            #region Fiyat Kontrolu

            if (input.Fiyat == null)
            {
                throw new Exception("Fiyat sifirdan buyuk olmalidir");
            }

            if (input.Fiyat <= 0)
            {
                throw new Exception("Fiyat sifirdan buyuk olmalidir");
            }

            #endregion
            return base.Insert(input);

        }
    }
}
