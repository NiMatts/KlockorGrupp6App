using System.ComponentModel.DataAnnotations;

namespace KlockorGrupp6App.Web.Views.Account;

public class RegisterVM
{
    [Required(ErrorMessage = "First Name is required")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Last Name is required")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "E-Mail is required")]
    [EmailAddress]
    [Display(Name = "E-Mail")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public bool isAdmin { get; set; } = false;

    [Required(ErrorMessage = "Password must be repeated")]
    [DataType(DataType.Password)]
    [Display(Name = "Repeat password")]
    [Compare(nameof(Password))]
    public string PasswordRepeat { get; set; } = null!;
}
