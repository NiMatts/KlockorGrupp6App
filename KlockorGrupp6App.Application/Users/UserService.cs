using KlockorGrupp6App.Application.Clocks.Interfaces;
using KlockorGrupp6App.Application.Dtos;

namespace KlockorGrupp6App.Application.Users;

public class UserService(IUnitOfWork unitOfWork) : IUserService
{
    public async Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password, bool isAdmin) =>
       await unitOfWork.IdentityUserService.CreateUserAsync(user,password,isAdmin);
    
    public async Task<UserResultDto> SignInAsync(string email, string password) =>
        await unitOfWork.IdentityUserService.SignInAsync(email, password);
        
    public async Task SignOutAsync()
    {
        await unitOfWork.IdentityUserService.SignOutAsync();
    }
    public async Task<UserProfileDto[]> GetAllUsersAsync()
    {
       return await unitOfWork.IdentityUserService.GetAllUsersAsync();
    }
}
