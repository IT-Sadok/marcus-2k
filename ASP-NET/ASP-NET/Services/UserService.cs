using ASP_NET.Dto;
using ASP_NET.Entities;
using Microsoft.AspNetCore.Identity;

namespace ASP_NET.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        this._userManager = userManager;
    }

    public async Task<User> CreateUser(RegisterDto dto)
    {

        var user = new User { Email = dto.Email, UserName = dto.Username };

        await this._userManager.CreateAsync(user, dto.Password);

        return user;
    }
}

public interface IUserService
{
    Task<User> CreateUser(RegisterDto dto);
}
