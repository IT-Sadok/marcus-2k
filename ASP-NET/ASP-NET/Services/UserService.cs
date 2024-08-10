using ASP_NET.Dto;
using ASP_NET.Entities;
using Microsoft.AspNetCore.Identity;

namespace ASP_NET.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(UserManager<User> userManager, IPasswordHasher<User> passwordHasher)
    {
        this._userManager = userManager;
        this._passwordHasher = passwordHasher;
    }

    public async Task<User> CreateUser(RegisterDto dto)
    {

        var user = new User { Email = dto.Email, UserName = dto.Username };

        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

        await this._userManager.CreateAsync(user);

        return user;
    }
}

public interface IUserService
{
    Task<User> CreateUser(RegisterDto dto);
}

