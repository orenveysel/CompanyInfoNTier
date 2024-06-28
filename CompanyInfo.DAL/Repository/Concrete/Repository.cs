using CompanyInfo.DAL.Repository.Abstract;
using CompanyInfo.Entities.DbContexts;
using CompanyInfo.Entities.Models.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CompanyInfo.DAL.Repository.Concrete
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {

        readonly AppDbContext _dbContext;
        readonly DbSet<T> _dbSet;
        public Repository()
        {
            _dbContext = new AppDbContext();
            _dbSet = _dbContext.Set<T>();
        }

        public virtual int Insert(T input)
        {
            /*
             * Burada ki Set<T> metodu 
             * dbcontext uzerindeki tanimli olan DbSet<T> propertiy'sine 
             * konumlanir
             * 
             */
            _dbContext.Set<T>().Add(input);
           // _dbContext.Urunler.Add(input);
            return _dbContext.SaveChanges();
        }

        public virtual int Update(T input)
        {
            _dbSet.Update(input);
           return  _dbContext.SaveChanges();
        }
        public virtual int Delete(T input)
        {
            _dbSet.Remove(input);
           return  _dbContext.SaveChanges();
        }

        public virtual int DeleteById(int id)
        {
           var silinecek =  _dbSet.Find(id);
            _dbSet.Remove(silinecek);
            return _dbContext.SaveChanges();
        }

        public virtual T? Get(Expression<Func<T, bool>> predicate )
        {
           return  _dbContext.Set<T>().Where(predicate).FirstOrDefault();

        }

        public virtual List<T>? GetAll(Expression<Func<T, bool>> predicate = null)
        {
            if(predicate!=null)
                return _dbContext.Set<T>().Where(predicate).ToList();
            else 
                return _dbContext.Set<T>().ToList(); 
        }

        public virtual T? GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public  IQueryable<T> GetAllInclude(Expression<Func<T, bool>>? predicate, params Expression<Func<T, object>>[] include)
        {
            IQueryable<T> query;
            if (predicate != null)
            {
                query = _dbContext.Set<T>().Where(predicate);
            }
            else
            {
                query = _dbContext.Set<T>();
            }

            return include.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

    }
}
