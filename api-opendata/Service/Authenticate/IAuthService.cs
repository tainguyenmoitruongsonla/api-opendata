using Microsoft.AspNetCore.Identity;
using api_opendata.Dto;
using api_opendata.Dto.Authenticate;

namespace api_opendata.Service
{
    public interface IAuthService
    {
        public Task<bool> RegisterAsync(UserDto dto);
        public Task<string> LoginAsync(LoginViewDto dto);
        public Task<bool> LogoutAsync(HttpContext context);
        public Task<bool> AssignRoleAsync(AssignRoleDto dto);
        public Task<bool> RemoveRoleAsync(AssignRoleDto dto);
        public Task<bool> UpdatePasswordAsync(string currentPassword, string newPassword, string newConfirmPassword);
        public Task<bool> SetPasswordAsync(UserDto dto, string newPassword);

    }
}
