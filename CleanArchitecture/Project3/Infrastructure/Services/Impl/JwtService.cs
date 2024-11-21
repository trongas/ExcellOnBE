using Domain.Abstraction;
using Infrastructure.Common;
using Infrastructure.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services.Impl
{
    public class JwtService : IJwtService
    {
        private readonly JwtConfig _jwtConfig;
        private readonly IUnitOfWork _unitOfWork;
        public readonly UserManager<IdentityUser> _userManager;
        public JwtService(
            IOptionsMonitor<JwtConfig> optionsMonitor,
            UserManager<IdentityUser> userManager,
            IUnitOfWork unitOfWork
            )
        {
            _jwtConfig = optionsMonitor.CurrentValue;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> GenerateJwtTokenAsync(IdentityUser user)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var roles = await _userManager.GetRolesAsync(user);
            var userRole = roles.FirstOrDefault();

            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
            };
            if (!string.IsNullOrEmpty(userRole))
            {
                tokenDescription.Subject.AddClaim(new Claim(ClaimTypes.Role, userRole));
            }
            else
            {
                tokenDescription.Subject.AddClaim(new Claim(ClaimTypes.Role, "User"));
            }

            var token = jwtHandler.CreateToken(tokenDescription);
            var jwtToken = jwtHandler.WriteToken(token);

            return jwtToken;
        }
        
        public async Task<UserProfileDto> GetProfileByJwt(string token)
        {
            try
            {
                var jwtHandler = new JwtSecurityTokenHandler();

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfig.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };

                var principal = jwtHandler.ValidateToken(token, validationParameters, out _);
                Console.WriteLine("Token Information: " + principal?.ToString());
                var claims = principal?.Claims;
                if (claims != null)
                {
                    foreach (var claim in claims)
                    {
                        Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
                    }
                }
                var emailClaim = principal.FindFirst(ClaimTypes.Email);

                if (emailClaim != null)
                {
                    var email = emailClaim.Value; // Sử dụng thuộc tính Value để lấy giá trị email thực tế
                    Console.WriteLine("Email: " + email);

                    var roleClaim = principal.FindFirst(ClaimTypes.Role);
                    Console.WriteLine("Role: " + roleClaim?.Value);

                    if (roleClaim != null)
                    {
                        var role = roleClaim.Value;

                        if (role == "Admin" || role == "Employee")
                        {
                            var userProfile = await _unitOfWork.Employees.GetByEmail(email);
                            var user = await _userManager.FindByEmailAsync(email);
                         var userProfileDto = new UserProfileDto()
                            {
                                Id = userProfile.EmployeeId,
                                Email = userProfile.Email,
                                Dob = userProfile.Dob,
                                FirstName = userProfile.FirstName,
                                LastName = userProfile.LastName,
                                Phone = userProfile.Phone,
                                Role = role,
                                UserAspId = user.Id
                            };
                            return userProfileDto;
                        }
                        else if (role == "User")
                        {
                            var userProfile = await _unitOfWork.Clients.GetClientByEmail(email);
                            var user = await _userManager.FindByEmailAsync(email);
                            var userProfileDto = new UserProfileDto()
                            {
                                Id = userProfile.ClientId,
                                Email = userProfile.Email,
                                Dob = userProfile.Dob,
                                FirstName = userProfile.FirstName,
                                LastName = userProfile.LastName,
                                Phone = userProfile.Phone,
                                Role = role,
                                UserAspId = user.Id
                            };
                            return userProfileDto;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi xác thực token
                Console.WriteLine($"Lỗi giải mã JWT: {ex.Message}");
                return null;
            }
        }

    }
}
