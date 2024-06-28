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
    public class RoleConfig:BaseConfig<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.RoleAdi).HasMaxLength(50);
            builder.HasIndex(p => p.RoleAdi).IsUnique();

            builder.HasData(
                new Role { Id = 1, RoleAdi = "Admin" }, 
                new Role { Id = 2, RoleAdi = "User" }, 
                new Role { Id = 3, RoleAdi = "Satis" });
        }
    }
}
