using CompanyInfo.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyInfo.Entities.Models.Concrete
{
    public class Menu:BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ActionName { get; set; }      // Index gibi.
        public string? ControllerName { get; set; }  // Home gibi.
        public string?  Area { get; set; }
        public string? QueryStrings { get; set; } // "?lang=en&search=abc" gibi.
        public int? OrderNo { get; set; } //Sira Numarasi
        public int? ParentId { get; set; }
        public string? Css { get; set; }
        public string? Class { get; set; }
        public virtual Menu? ParentMenu { get; set; }
        public virtual List<Menu>? Menuler { get; set; }
    }
}
