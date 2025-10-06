using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SportsStore.Models.ViewModels

{
    public class CheckoutViewModel : IValidatableObject
    {
        [Required(ErrorMessage ="Please enter a name ")]
        [StringLength(100)]
       public string ? Name { get; set; }

        [Required(ErrorMessage ="Please enter the first adderss line ")]
        [StringLength(200)]
       public string ? Line1 { get; set; }

        [StringLength(200)]
       public string ? Line2 { get; set; }

        [StringLength(200)]
        public string? Line3 { get; set; }

        [Required(ErrorMessage ="Please enter a city name ")]
        [StringLength (100)]
       public string ? City { get; set; }

        [Required(ErrorMessage = "Please enter a state name ")]
        [StringLength(100)]
        public string? State { get; set; }

        [StringLength (20)]
        public string? Zip { get; set; }

        [Required(ErrorMessage = "Please enter a country name ")]
        [StringLength(100)]
        public string? Country { get; set; }

        public bool GiftWrap { get; set; }

        [Required(ErrorMessage ="Please enter a phone number ")]
        public string ? Phone { get; set; }

    
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(Name) &&
                Country?.Trim().ToLowerInvariant() == "usa" &&
               string.IsNullOrWhiteSpace(Zip))
            {
                yield return new ValidationResult("Zip is required for USA", new[] {nameof(Zip)});
            }
        }





    }
}
