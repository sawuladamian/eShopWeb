using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Models
{
     public class ShoppingCart
    {
        
        public int Id { get; set; }
        
        public int TshirtId { get; set; }
        [ForeignKey("TshirtId")]
        [ValidateNever]
        public Tshirt Tshirt{ get; set; }
        [Range(1,1000)]
        public int Count { get; set; }
        
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        [NotMapped]
        public double  Price { get; set; }
        [NotMapped]
        public IEnumerable<Tshirt> ColorList { get; set; }
    }
}
