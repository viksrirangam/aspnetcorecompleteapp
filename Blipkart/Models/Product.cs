using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Blipkart.Model
{
    public class Product : Entity<long>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public double UnitPrice { get; set; }
    }
}
