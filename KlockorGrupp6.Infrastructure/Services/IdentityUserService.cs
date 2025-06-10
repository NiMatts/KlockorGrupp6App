using KlockorGrupp6App.Application.Dtos;
using KlockorGrupp6App.Application.Users;
using KlockorGrupp6App.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlockorGrupp6App.Infrastructure.Services
{
    public class IdentityUserService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager) : IIdentityUserService
    {
        public async Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password)
        {
            var result = await userManager.CreateAsync(new ApplicationUser
            {
                UserName = user.Email,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            }, password);

            return new UserResultDto(result.Errors.FirstOrDefault()?.Description);
        }

        public async Task<UserResultDto> SignInAsync(string email, string password)
        {
            var result = await signInManager.PasswordSignInAsync(email, password, false, false);
            return new UserResultDto(result.Succeeded ? null : "Invalid user credentials");
        }

        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }
    }
}
