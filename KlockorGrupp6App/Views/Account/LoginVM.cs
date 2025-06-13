using System.ComponentModel.DataAnnotations;

namespace KlockorGrupp6App.Web.Views.Account;

public class LoginVM
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
