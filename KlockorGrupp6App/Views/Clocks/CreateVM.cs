using System.ComponentModel.DataAnnotations;

namespace KlockorGrupp6App.Web.Views.Clocks
{
    public class CreateVM
    {
        [Required(ErrorMessage = "Enter a Brand")]
        public required string Brand { get; set; }

        [Required(ErrorMessage = "Enter a Model")]
        public required string Model { get; set; }

        [Required(ErrorMessage = "Enter a Price")]
        public required decimal Price { get; set; }

        [Required(ErrorMessage = "Enter a Year")]        
        public required uint Year { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ArgumentNullException.ThrowIfNull(validationContext);
            int currentYear = DateTime.Now.Year;

            if (Year < 1550 || Year > currentYear)
            {
                yield return new ValidationResult(
                    $"Enter a valid year between 1550 and {currentYear}.",
                    new[] { nameof(Year) });
            }
        }
    }
}
