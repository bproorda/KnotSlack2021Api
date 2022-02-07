using knotslack2022api.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace knotslack2022api.Services
{
    public class UserManager : IUserManager
    {
        public Task AccessFailedAsync(KSUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AdminCheck()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckPasswordAsync(KSUser user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(KSUser user, string password, string role)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateToken(KSUser user)
        {
            throw new NotImplementedException();
        }

        public Task<KSUser> FindAllLoggedInUsers()
        {
            throw new NotImplementedException();
        }

        public Task<KSUser> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<KSUser> FindByNameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetUserRoles(KSUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUserAdmin(KSUser user)
        {
            throw new NotImplementedException();
        }
    }
}
