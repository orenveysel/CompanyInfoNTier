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
    public class MenuConfig:BaseConfig<Menu>
    {

        public override void Configure(EntityTypeBuilder<Menu> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.QueryStrings).HasMaxLength(500);
            builder.Property(p => p.ActionName).HasMaxLength(50);
            builder.Property(p => p.ControllerName).HasMaxLength(50);
            builder.Property(p => p.Description).HasMaxLength(100);
            builder.Property(p => p.Class).HasMaxLength(1000);
            builder.Property(p => p.Area).HasMaxLength(50);

            builder.HasData(new Menu { 
                Id = 1, 
                Description = "Urun Yonetimi", 
                Name = "Urun Yonetimi", 
                ActionName = "", 
                ControllerName = "", 
                Area = "", 
                ParentId = null 
            });
            builder.HasData(new Menu { 
                Id = 2, 
                Description = "Urunler", 
                Name = "Urunler", 
                ActionName = "Index", 
                ControllerName = "Urun", 
                Area = "Admin", 
                ParentId = 1 
            });
            builder.HasData(new Menu { 
                Id = 3, 
                Description = "Kategoriler", 
                Name = "Kategoriler", 
                ActionName = "Index", 
                ControllerName = "Kategori", 
                Area = "Admin", 
                ParentId = 1 
            });
            builder.HasData(new Menu
            {
                Id = 4,
                Description = "Urunlere Kategori Ekle",
                Name = "Urunlere Kategori Ekleme",
                ActionName = "KategoriYonet",
                ControllerName = "Urun",
                Area = "Admin",
                ParentId = 1
            });

            builder.HasData(new Menu { 
                Id = 5, 
                Description = "Kategorilere Urun Ekle", 
                Name = "Kategorilere Urun Ekleme", 
                ActionName = "UrunYonet", 
                ControllerName = "Kategori", 
                Area = "Admin", 
                ParentId = 1 
            });

            builder.HasData(new Menu { 
                Id = 6, 
                Description = "Tedarikciler", 
                Name = "Tedarikciler", 
                ActionName = "", 
                ControllerName = "", 
                Area = "", 
                ParentId = null 
            });

            builder.HasData(new Menu { 
                Id = 7, 
                Description = "Tedarikci Ekle", 
                Name = "Tedarikci Ekle", 
                ActionName = "Create", 
                ControllerName = "Tedarikci", 
                Area = "Admin", 
                ParentId = 6 
            });







        }
    }
}
