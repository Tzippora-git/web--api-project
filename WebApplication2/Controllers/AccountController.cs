using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication2.Models.DTO;
using WebApplication2.BLL;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserBll _userBll;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserBll userBll, IConfiguration configuration, ILogger<AccountController> logger)
        {
            _userBll = userBll;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            _logger.LogInformation("Login attempt for email: {Email}", login.Email);

            try
            {
                var user = await _userBll.ValidateUser(login.Email, login.Password);

                if (user != null)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var jwtSecretKey = _configuration["Jwt:SecretKey"] ?? "YourSuperSecretKeyHere1234567890!";
                    var key = Encoding.ASCII.GetBytes(jwtSecretKey);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Name, user.Name),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Role, user.Role)
                        }),
                        Expires = DateTime.UtcNow.AddHours(2),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    _logger.LogInformation("Login successful for email: {Email}", login.Email);

                    return Ok(new
                    {
                        token = tokenHandler.WriteToken(token),
                        user = new
                        {
                            id = user.Id,
                            name = user.Name,
                            email = user.Email,
                            role = user.Role
                        }
                    });
                }

                _logger.LogWarning("Invalid login attempt for email: {Email}", login.Email);
                return Unauthorized("Invalid username or password");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for email: {Email}", login.Email);
                return StatusCode(500, new { error = "An error occurred while processing your request." });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            _logger.LogInformation("Registration attempt for email: {Email}", userDto?.Email);

            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for registration");
                    return BadRequest(ModelState);
                }

                // אם המייל הוא של מנהל, תן לו תפקיד מנהל
                if (userDto.Email == "admin@admin.com" || userDto.Role == "Manager")
                {
                    userDto.Role = "Manager";
                    _logger.LogInformation("Role set to Manager for email: {Email}", userDto?.Email);
                }

                await _userBll.AddUser(userDto);
                _logger.LogInformation("User registered successfully: {Email}", userDto.Email);

                // התחבר אוטומטית אחרי הרשמה
                var user = await _userBll.ValidateUser(userDto.Email, userDto.Password);
                if (user != null)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var jwtSecretKey = _configuration["Jwt:SecretKey"] ?? "YourSuperSecretKeyHere1234567890!";
                    var key = Encoding.ASCII.GetBytes(jwtSecretKey);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Name, user.Name),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Role, user.Role)
                        }),
                        Expires = DateTime.UtcNow.AddHours(2),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    _logger.LogInformation("User {Email} logged in automatically after registration", user.Email);

                    return Ok(new
                    {
                        token = tokenHandler.WriteToken(token),
                        user = new
                        {
                            id = user.Id,
                            name = user.Name,
                            email = user.Email,
                            role = user.Role
                        },
                        message = "User registered successfully"
                    });
                }


                return Ok(new { message = "User registered successfully. You can now log in." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Registration error for email: {Email}", userDto?.Email);
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
