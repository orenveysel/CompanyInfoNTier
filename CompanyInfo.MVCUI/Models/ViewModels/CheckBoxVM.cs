using CompanyInfo.Entities.Models.Concrete;

namespace CompanyInfo.MVCUI.Models.ViewModels
{
    public class CheckBoxVM
    {
        public int Id { get; set; }
        public string  LabelName { get; set; }
        public bool IsChecked { get; set; }
        public int? EntityId { get; set; }
       
        
    }
}
