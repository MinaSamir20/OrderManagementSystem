using OrderManagementSystem.Application.DTOs.Authentication;

namespace OrderManagementSystem.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<ResponseDto> CreateRoleAsync(string role);
        Task<AuthModel> RegisterAsync(RegisterDto model);
        Task<AuthModel> LoginAsync(LoginDto loginDto);
        Task<string> AddRoleToUserAsync(AddRoleModel model);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(string id);
        Task<string> UpdateUserAsync(UserDto dto);
        Task<string> DeleteUserAsync(string id);
        Task<bool> ChangePassword(string id, string currentPassword, string newPassword);
        Task<AuthModel> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
    }
}
