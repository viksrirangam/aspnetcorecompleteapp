using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Blipkart.ViewModel
{
    public class Cart
    {
        public string CartId {get; set;}

        public long CustomerId {get; set;}

        [Display(Name="Customer Name")]
        public string CustomerName {get; set;}

        public List<LineItem> LineItems = new List<LineItem>();

        [Display(Name="Grand Total")]
        public double GrandTotal
        {
            get{
                return LineItems.Sum(i => i.Price * i.Quantity);
            }
        }

        public bool IsEmpty
        {
            get{
                return !LineItems.Any();
            }
        }
    }
}
