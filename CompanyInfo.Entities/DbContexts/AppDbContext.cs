using CompanyInfo.Entities.EntityConfig.Concrete;
using CompanyInfo.Entities.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CompanyInfo.Entities.DbContexts
{
    public class AppDbContext:DbContext
    {

        public DbSet<Urun>  Urunler{ get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Tedarikci> Tedarikciler { get; set; }
        public DbSet<Birim> Birimler { get; set; }
        public DbSet<Menu> Menuler { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roller{ get; set; }

        public DbSet<UrunFoto> UrunFotolari { get; set; }
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=IstkaCompanyInfo;Trusted_Connection=true;
                TrustServerCertificate=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new BirimConfig());
            //modelBuilder.ApplyConfiguration(new KategoriConfig());
           
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        }
    }
}
