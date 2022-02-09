using knotslack2022api.Models.Identity;
using knotslack2022api.Models.Identity.DTO;
using Microsoft.AspNetCore.Identity;

namespace knotslack2022api.Services
{
    public interface IUserManager
    {
        Task<bool> CheckPasswordAsync(KSUser user, string password);
        Task<IdentityResult> CreateAsync(KSUser user, string password, string role = "user");
        Task<KSUser> FindByIdAsync(string userId);
        Task<KSUser> FindByNameAsync(string username);
        Task<IdentityResult> UpdateAsync(KSUser user);
        Task<string> CreateToken(KSUser user);
        Task InitializeRolesAsync();
    }
}
