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
    public class TedarikciConfig:BaseConfig<Tedarikci>
    {
        public override void Configure(EntityTypeBuilder<Tedarikci> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.SirketAdi).HasMaxLength(50);
            builder.Property(p=>p.VergiNo).HasMaxLength(20);
            builder.HasIndex(p => p.VergiNo).IsUnique();

            builder.HasData(
                new Tedarikci { Id = 1, SirketAdi = "Abc", VergiNo = "123", CreateDate = DateTime.Now },
                new Tedarikci { Id = 2, SirketAdi = "Asd", VergiNo = "456", CreateDate = DateTime.Now }, 
                new Tedarikci { Id = 3, SirketAdi = "Qwe", VergiNo = "7789", CreateDate = DateTime.Now });
        }
    }
}
