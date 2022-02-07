using knotslack2022api.Models.Identity;
using Knotslack2022api.Data;
using Microsoft.AspNetCore.Identity;

namespace knotslack2022api.Services
{
    public class UserManager : IUserManager
    {
        private readonly UserManager<KSUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private KnotSlack2022DbContext _context;

        public UserManager(UserManager<KSUser> userManager, IConfiguration configuration, KnotSlack2022DbContext context, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
            this._context = context;
        }

        public Task AccessFailedAsync(KSUser user)
        {
            return userManager.AccessFailedAsync(user);
        }

        public async Task<bool> AdminCheck()
        {
            if (!await roleManager.RoleExistsAsync("admin"))
            {
                var newRole = new IdentityRole();
                newRole.Name = "admin";
                await roleManager.CreateAsync(newRole);

                return false;
            }
            else
            {
                var admins = await userManager.GetUsersInRoleAsync("admin");

                if (admins.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public Task<bool> CheckPasswordAsync(KSUser user, string password)
        {
            return userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> CreateAsync(KSUser user, string password, string role)
        {
            if (role == "user" && !_context.Roles.Any(r => r.Name == "user"))
            {
                var newRole = new IdentityRole();
                newRole.Name = "user";
                await roleManager.CreateAsync(newRole);
            }

            var result = await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user, role);
            //await AddNewUserToGeneral(user.UserName);

            return result;
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
