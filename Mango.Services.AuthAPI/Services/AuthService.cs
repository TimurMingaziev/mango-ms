using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.AuthAPI.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<UserDto> Register(RegistrationRequestDto registrationRequestDto)
    {
        ApplicationUser user = new()
        {
            UserName = registrationRequestDto.Email.ToUpper(),
            Email = registrationRequestDto.Email,
            NormalizedEmail = registrationRequestDto.Email.ToUpper(),
            Name = registrationRequestDto.Name,
            PhoneNumber = registrationRequestDto.PhoneNumber
        };

        try
        {
            var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
            if (result.Succeeded)
            {
                var userToReturn = await _dbContext.ApplicationUsers
                    .FirstOrDefaultAsync(u => u.UserName == registrationRequestDto.Email.ToUpper());

                if (userToReturn == null)
                {
                    return new UserDto();
                }

                UserDto userDto = new()
                {
                    Email = userToReturn.Email,
                    PhoneNumber = userToReturn.PhoneNumber,
                    ID = userToReturn.Id,
                    Name = userToReturn.Name
                };

                return userDto;
            }
        }
        catch (Exception ex)
        {

        }

        return new UserDto();
    }

    public Task<LoginResponseDto> Login(LoginResponseDto registrationRequestDto)
    {
        throw new NotImplementedException();
    }
}