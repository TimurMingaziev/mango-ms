using Mango.Services.AuthAPI.Models.Dto;

namespace Mango.Services.AuthAPI.Services.IServices;

public interface IAuthService
{
    public Task<UserDto> Register(RegistrationRequestDto registrationRequestDto);
    public Task<LoginResponseDto> Login(LoginResponseDto registrationRequestDto);
}