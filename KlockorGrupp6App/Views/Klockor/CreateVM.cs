using System.ComponentModel.DataAnnotations;

namespace KlockorGrupp6App.Web.Views.Klockor
{
    public class CreateVM
    {
        [Required(ErrorMessage = "Enter a Brand")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Enter a Model")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Enter a Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Enter a Year")]
        public DateTime Year { get; set; }
    }
}
