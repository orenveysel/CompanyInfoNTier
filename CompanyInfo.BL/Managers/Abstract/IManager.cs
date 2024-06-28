using CompanyInfo.DAL.Repository.Abstract;
using CompanyInfo.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyInfo.BL.Managers.Abstract
{
    public interface IManager<T>:IRepository<T> where T : BaseEntity
    {
    }
}
