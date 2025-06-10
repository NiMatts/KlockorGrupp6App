using KlockorGrupp6App.Application.Dtos;

namespace KlockorGrupp6App.Application.Users
{
    public interface IIdentityUserService
    {
        Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password);
        Task<UserResultDto> SignInAsync(string email, string password);
        Task SignOutAsync();
    }
}