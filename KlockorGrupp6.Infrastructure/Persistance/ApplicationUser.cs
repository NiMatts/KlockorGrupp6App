using KlockorGrupp6App.Domain;
using Microsoft.AspNetCore.Identity;

namespace KlockorGrupp6App.Infrastructure.Persistance
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName{ get; set; } = null!;
        public string LastName { get; set; } = null!;

        public ICollection<Clock> Clocks { get; set; }
    }
}
