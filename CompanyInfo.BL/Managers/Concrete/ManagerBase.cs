using CompanyInfo.BL.Managers.Abstract;
using CompanyInfo.DAL.Repository.Concrete;
using CompanyInfo.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyInfo.BL.Managers.Concrete
{
    public class ManagerBase<T>:Repository<T>, IManager<T> where T : BaseEntity
    {
    }
}
