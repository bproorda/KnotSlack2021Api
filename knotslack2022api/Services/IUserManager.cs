using knotslack2022api.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace knotslack2022api.Services
{
    public interface IUserManager
    {
        Task<KSUser> FindByNameAsync(string username);
        Task<bool> CheckPasswordAsync(KSUser user, string password);
        Task AccessFailedAsync(KSUser user);
        Task<IdentityResult> CreateAsync(KSUser user, string password, string role);
        Task<bool> AdminCheck();
        Task<bool> IsUserAdmin(KSUser user);
        Task<IList<string>> GetUserRoles(KSUser user);
        Task<KSUser> FindByIdAsync(string userId);
        Task<KSUser> FindAllLoggedInUsers();
        //Task<IdentityResult> UpdateAsync(KSUser user);
        //Task<IdentityResult> DeleteUser(string username);
        //Task<UserWithToken> CreateUserWithToken(KSUser user);
        //Task<UserWithoutToken> CreateUserWithoutToken(KSUser user);
        //Task<List<createChannelDTO>> GetUserChannels(KSUser user);
        //Task<UserChannel> AddNewUserToGeneral(string username);
        Task<string> CreateToken(KSUser user);
    }
}
