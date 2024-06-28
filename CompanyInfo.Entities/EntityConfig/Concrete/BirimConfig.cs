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
    public class BirimConfig:BaseConfig<Birim>
    {
        public override void Configure(EntityTypeBuilder<Birim> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.BirimAdi).HasMaxLength(50);
            //Ayni Birimden 2. kere olmasin
            builder.HasIndex(p => p.BirimAdi).IsUnique();

            builder.HasData(new Birim { Id = 1, BirimAdi = "Adet", CreateDate = DateTime.Now },
                new Birim { Id = 2, BirimAdi = "Cm", CreateDate = DateTime.Now },
                new Birim { Id = 3, BirimAdi = "Gram", CreateDate = DateTime.Now },
                new Birim { Id = 4, BirimAdi = "Miligram", CreateDate = DateTime.Now });
        }
    }
}
