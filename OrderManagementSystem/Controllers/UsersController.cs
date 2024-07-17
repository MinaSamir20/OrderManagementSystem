using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.DTOs.Authentication;
using OrderManagementSystem.Application.Services.Auth;

namespace OrderManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IAuthService auth) : ControllerBase
    {
        private readonly IAuthService _auth = auth;

        [HttpPost, Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _auth.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            SetRefreshTokenInCookie(result.RefreshToken!, result.RefreshTokenExpiration);

            return Ok(result);
        }

        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _auth.LoginAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            CookieOptions cookieOptions = new()
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime(),
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        [HttpGet, Route("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var result = await _auth.RefreshTokenAsync(refreshToken!);

            if (result.IsAuthenticated)
                return BadRequest(result);

            SetRefreshTokenInCookie(result.RefreshToken!, result.RefreshTokenExpiration);
            return Ok(result);
        }

        [HttpPost, Route("revokeToken")]
        public async Task<IActionResult> RevokeToken(RevokeToken revoke)
        {
            var token = revoke.Token ?? Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token)) return BadRequest("Token is Required");

            var result = await _auth.RevokeTokenAsync(token);

            if (!result) return BadRequest("Token is Invalid!");
            return Ok();
        }
    }
}
