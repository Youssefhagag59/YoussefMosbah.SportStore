using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models
{
    public class Order 
    {
        [BindNever]
        public int OrderID { get; set; }
         
        [BindNever]
        public IEnumerable<CartLine> CartLines { get; set; } = new List <CartLine>();

        [BindNever]
        public bool Shipped { get; set; }

 
       public string? Name { get; set; }

        public string? Line1 { get; set; }

        public string? Line2 { get; set; }

        public string? Line3 { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Zip { get; set; }

        public string? Country { get; set; }

        public bool GiftWrap { get; set; }

        [Phone(ErrorMessage = "Please enter a phone number ")]
        public string? Phone { get; set; }

    }
}
