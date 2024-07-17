using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrderManagementSystem.Application.DTOs.Authentication;
using OrderManagementSystem.Application.Helper;
using OrderManagementSystem.Domain.Entities.Identity;
using OrderManagementSystem.Infrastructure.Databases;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OrderManagementSystem.Application.Services.Auth
{
    public class AuthService(AppDbContext db, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt) : IAuthService
    {
        private readonly AppDbContext _db = db;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<User> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly JWT _jwt = jwt.Value;
        public async Task<string> AddRoleToUserAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId!);
            if (user == null || !await _roleManager.RoleExistsAsync(model.Role!))
                return "Invalid User ID or Role";
            if (await _userManager.IsInRoleAsync(user, model.Role!))
                return "User already assigned to this Role";
            var result = await _userManager.AddToRoleAsync(user, model.Role!);
            return result.Succeeded ? string.Empty : "Something went wrong";
        }

        public async Task<bool> ChangePassword(string id, string currentPassword, string newPassword)
        {
            var user = await _db.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (user == null) return false;
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return result.Succeeded;
        }

        public async Task<ResponseDto> CreateRoleAsync(string role)
        {
            bool isRoleExists = await _roleManager.RoleExistsAsync(role.ToUpper());
            if (isRoleExists)
                return new()
                {
                    IsSuccess = false,
                    DisplayMessage = $"{role.ToUpper()} Role is Already Seeding Before"
                };
            await _roleManager.CreateAsync(new IdentityRole(role.ToUpper()));
            return new()
            {
                IsSuccess = true,
                DisplayMessage = $"{role.ToUpper()} Role is Done Successfully"
            };
        }

        public async Task<string> DeleteUserAsync(string id)
        {
            var result = await _db.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (result!.UserName == "Admin@24") return "Unable to Delete Admin User";
            else if (result != null) _db.Users.Remove(result!);
            return "Deleted";
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _db.Users.ToListAsync();
            return users.Select(u => new UserDto
            {
                Email = u.Email,
                UserName = u.UserName,
            });
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var user = await _db.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            return new UserDto
            {
                Email = user!.Email,
                UserName = user.UserName,
            };
        }

        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var roleClaims = userRoles.Select(r => new Claim("roles", r)).ToList();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key)), SecurityAlgorithms.HmacSha256);

            return new(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays!),
                signingCredentials: signingCredentials);
        }

        private static RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
#pragma warning disable SYSLIB0023 // Type or member is obsolete
            using RNGCryptoServiceProvider generator = new();
            generator.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(10),
                CreatedOn = DateTime.UtcNow,
            };
        }

        public async Task<AuthModel> LoginAsync(LoginDto loginDto)
        {
            AuthModel authModel = new();

            var user = await _userManager.FindByEmailAsync(loginDto.Email!);

            if (user is null || !await _userManager.CheckPasswordAsync(user, loginDto.Password!))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            //authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Roles = [.. (await _userManager.GetRolesAsync(user))];

            if (user.RefreshTokens!.Any(t => t.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens!.FirstOrDefault(t => t.IsActive);
                authModel.RefreshToken = activeRefreshToken!.Token;
                authModel.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
            }
            else
            {
                var refreshToken = GenerateRefreshToken();
                authModel.RefreshToken = refreshToken.Token;
                authModel.RefreshTokenExpiration = refreshToken.ExpiresOn;
                user.RefreshTokens!.Add(refreshToken);
                await _userManager.UpdateAsync(user);
            }

            return authModel;
        }

        public async Task<AuthModel> RefreshTokenAsync(string token)
        {
            var authModel = new AuthModel();
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens!.Any(t => t.Token == token));
            if (user == null)
            {
                authModel.Message = "Invalid Token";
                return authModel;
            }

            var refreshToken = user.RefreshTokens!.Single(t => t.Token == token);
            if (!refreshToken.IsActive)
            {
                authModel.Message = "Inactive Token";
                return authModel;
            }

            refreshToken.RevokedOn = DateTime.UtcNow;

            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens!.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            var jwtToken = await CreateJwtToken(user);
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.Roles = [.. await _userManager.GetRolesAsync(user)];
            authModel.RefreshToken = refreshToken.Token;
            authModel.RefreshTokenExpiration = refreshToken.ExpiresOn;

            return authModel;
        }

        public async Task<AuthModel> RegisterAsync(RegisterDto model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new() { Message = $"{model.Email} is already registered!" };
            if (await _userManager.FindByNameAsync(model.UserName) is not null)
                return new() { Message = $"{model.UserName} is already registered!" };

            var user = _mapper.Map<User>(model);

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                    errors += $"{error.Description},";
                return new() { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, "User");
            var jwtSecurityToken = await CreateJwtToken(user);

            var authModel = new AuthModel()
            {
                Email = user.Email,
                //ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = ["User"],
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName,
            };
            return authModel;
        }

        public async Task<bool> RevokeTokenAsync(string token)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens!.Any(t => t.Token == token));
            if (user == null)
                return false;

            var refreshToken = user.RefreshTokens!.Single(t => t.Token == token);
            if (!refreshToken.IsActive)
                return false;

            refreshToken.RevokedOn = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);

            return true;
        }

        public async Task<string> UpdateUserAsync(UserDto dto)
        {
            if (dto!.UserName == "Admin@24") return "Unable to Update Admin User";
            _db.Entry(dto).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return "Updated";
        }
    }
}
