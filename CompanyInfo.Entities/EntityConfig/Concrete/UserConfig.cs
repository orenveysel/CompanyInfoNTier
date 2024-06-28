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
    public class UserConfig:BaseConfig<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Password).HasMaxLength(100);
            builder.Property(p => p.Ad).HasMaxLength(50);
            builder.Property(p => p.Soyad).HasMaxLength(50);
            builder.Property(p => p.Email).HasMaxLength(100);
            builder.Property(p => p.Gsm).HasMaxLength(20);
            builder.Property(p => p.ImagePath).HasMaxLength(1000);

            builder.HasIndex(p => p.Email).IsUnique();
            builder.HasIndex(p => p.Gsm).IsUnique();





        }
    }
}
