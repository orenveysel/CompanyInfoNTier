using CompanyInfo.Entities.EntityConfig.Abstract;
using CompanyInfo.Entities.Models.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyInfo.Entities.EntityConfig.Concrete
{
    public class KategoriConfig:BaseConfig<Kategori>
    {
        public override void Configure(EntityTypeBuilder<Kategori> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.KategoriAdi).HasMaxLength(50);

            //Ayni kategoriden ikincisi olmasin
            builder.HasIndex(p => p.KategoriAdi).IsUnique();
            //Ornek Data
            builder.HasData(
                new Kategori { Id = 1, KategoriAdi = "Gida", CreateDate = DateTime.Now },
                new Kategori { Id = 2, KategoriAdi = "Tekstil", CreateDate = DateTime.Now },
                new Kategori { Id = 3, KategoriAdi = "Eletronik", CreateDate = DateTime.Now },
                new Kategori { Id = 4, KategoriAdi = "Otomotiv", CreateDate = DateTime.Now });



        }
    }
}
