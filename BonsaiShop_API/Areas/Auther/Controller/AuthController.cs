using AutoMapper;
using BonsaiShop_API.Areas.Auther.Model.Dtos;
using BonsaiShop_API.Areas.Auther.Model;
using BonsaiShop_API.Areas.Auther.Service;
using BonsaiShop_API.DALL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace BonsaiShop_API.Areas.Auther.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenBlacklistRepository _tokenBlacklistRepository;
        private readonly AuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IUserRepository userRepository, ITokenBlacklistRepository tokenBlacklistRepository, AuthService authService, IMapper mapper)
        {
            _userRepository = userRepository;
            _tokenBlacklistRepository = tokenBlacklistRepository;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO userDto)
        {
            if (await _userRepository.GetUserByEmail(userDto.Email) != null)
                return BadRequest("User already exists");

            var user = _mapper.Map<User>(userDto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            

            await _userRepository.AddUserAsync(user);
            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO userDto)
        {
            var user = await _userRepository.GetUserByEmail(userDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, user.PasswordHash))
                return Unauthorized();

            var token = _authService.GenerateToken(user);

            return Ok(new {
                UserName = user.UserName,
                Role = user.Role,
                Token = token
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
                return BadRequest("Token not provided");

            var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
            var expiration = jwtToken.ValidTo;

            // Thêm token vào Redis blacklist với thời gian hết hạn
            await _tokenBlacklistRepository.AddTokenToBlacklistAsync(token, expiration);

            return Ok(new { message = "Logged out successfully" });
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("update-role")]
        public async Task<IActionResult> UpdateUserRole(UpdateUserRoleDTO updateUserRoleDto)
        {
            var user = await _userRepository.GetUserById(updateUserRoleDto.UserId);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            user.Role = updateUserRoleDto.NewRole;
            await _userRepository.UpdateUser(user);

            return Ok(new { message = "User role updated successfully." });
        }


    }
}
