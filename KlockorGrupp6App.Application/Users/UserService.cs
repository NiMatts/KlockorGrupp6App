using KlockorGrupp6App.Application.Dtos;

namespace KlockorGrupp6App.Application.Users;

public class UserService(IIdentityUserService identityUserService) : IUserService
{
    public async Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password, bool isAdmin) =>
       await identityUserService.CreateUserAsync(user,password,isAdmin);

    public async Task<UserResultDto> SignInAsync(string email, string password) =>
        await identityUserService.SignInAsync(email, password);
        
    public async Task SignOutAsync()
    {
        await identityUserService.SignOutAsync();
    }
    public async Task<UserProfileDto[]> GetAllUsersAsync()
    {
       return await identityUserService.GetAllUsersAsync();
    }
}
