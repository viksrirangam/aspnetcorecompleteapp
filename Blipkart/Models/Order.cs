using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Blipkart.Model
{
    public class Order : Entity<long>
    {
        [Required]
        public long CustomerId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public double TotalAmount { get; set; }

        public Customer Customer {get; set;}

        public List<OrderItem> OrderItems {get; set;}
    }
}
