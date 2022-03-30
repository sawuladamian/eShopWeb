using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace eShop.Models.ViewModels
{
    public class TshirtVM
    {
        public Tshirt Tshirt { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ColorList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> SizeList { get; set; }
    }
}
