using System.ComponentModel.DataAnnotations;

namespace KlockorGrupp6App.Web.Views.Klockor
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
        public required DateTime Year { get; set; }
    }
}
