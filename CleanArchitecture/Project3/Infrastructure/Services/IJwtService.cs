using Infrastructure.Common;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public interface IJwtService
    {
        Task<string> GenerateJwtTokenAsync(IdentityUser user);
        Task<UserProfileDto> GetProfileByJwt(string token);
    }
}
