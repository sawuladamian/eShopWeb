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
    public class TshirtImagesUrl
    {
        [Key]
        public int UrlID { get; set; }
        [ForeignKey("TshirtID")]
        public int TshirtID { get; set; }
        [ValidateNever]
        public string ImageURL { get; set; }
        [ValidateNever]
        public string Color { get; set; }
    }
}
