using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Blipkart.Model
{
    public class OrderItem : Entity<long>
    {
        [Required]
        public long ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }

        public Product Product {get; set;}
    }
}
