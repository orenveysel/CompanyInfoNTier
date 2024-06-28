using CompanyInfo.Entities.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CompanyInfo.BL.Managers.Abstract
{
    public interface IUrunManager:IManager<Urun>
    {
        ICollection<Urun> KritikStokSeviyesiAltindakiler();
        
    }
}
