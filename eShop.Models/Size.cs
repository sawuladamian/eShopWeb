using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Models
{
    public class Size
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        
    }
}
