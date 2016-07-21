using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Blipkart.ViewModel
{
    public class Item
    {
        [Required]
        public long ProductId { get; set; }

        [Required]
        [Display(Name="Product Name")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name="Price")]
        public double Price { get; set; }
    }
}
