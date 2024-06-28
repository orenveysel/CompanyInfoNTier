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
    public class UrunFotoConfig:BaseConfig<UrunFoto>
    {
        public override void Configure(EntityTypeBuilder<UrunFoto> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.FileType).HasMaxLength(10);
            builder.Property(p => p.Name).HasMaxLength(100);
            builder.Property(p => p.FilePath).HasMaxLength(500);
            
            builder.HasIndex(p => p.Name);



        }
    }
}
