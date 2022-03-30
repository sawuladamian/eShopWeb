using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Tshirt> Tshirt { get; set; }
        public string FilterName { get; set; }
    }
}
