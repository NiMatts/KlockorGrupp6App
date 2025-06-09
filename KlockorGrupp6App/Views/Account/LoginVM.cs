using System.ComponentModel.DataAnnotations;

namespace KlockorGrupp6App.Web.Views.Account;

public class LoginVM
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
