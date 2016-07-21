using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Blipkart.Model
{
    public class Customer : AuditableEntity<long>
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public List<Order> Orders { get; set; }
    }
}
