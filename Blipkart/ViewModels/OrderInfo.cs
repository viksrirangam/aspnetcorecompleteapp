using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Blipkart.ViewModel
{
    public class OrderInfo
    {
        [Required]
        [Display(Name="Order#")]
        public long Id { get; set; }

        [Required]
        [Display(Name="Order Date")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name="Total Amount")]
        public double TotalAmount { get; set; }

        public List<LineItem> LineItems = new List<LineItem>();

        public double GrandTotal
        {
            get{
                return LineItems.Sum(i => i.Price * i.Quantity);
            }
        }
    }
}
