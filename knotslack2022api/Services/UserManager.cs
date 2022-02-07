using knotslack2022api.Models.Identity;
using knotslack2022api.Models.Identity.DTO;
using Knotslack2022api.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace knotslack2022api.Services
{ //TODO: update metods, especially ones concerning roles
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

        public async Task<string> CreateToken(KSUser user)
        {
            var secret = configuration["JWTSecret"];
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            var signingKey = new SymmetricSecurityKey(secretBytes);
            var roles = (List<string>)await userManager.GetRolesAsync(user);

            var tokenClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim("UserId", user.Id),
                new Claim(ClaimTypes.Role, roles[0]),
            };

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddSeconds(36000),
                claims: tokenClaims,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        public async Task<List<KSUserDTO>> FindAllLoggedInUsers()
        {
            var loggedInUsers = await _context.KSUsers
                .Where(u => u.LoggedIn).Select(u => 
                new KSUserDTO
                {
                    UserName = u.UserName,
                }
                ).ToListAsync();
            return loggedInUsers;
        }

        public async Task<KSUser> FindByIdAsync(string userId)
        {
            return await userManager.FindByIdAsync(userId);
        }

        public Task<KSUser> FindByNameAsync(string username)
        {
            return userManager.FindByNameAsync(username);
        }

        public async Task<IList<string>> GetUserRoles(KSUser user)
        {
            var thisUser = await FindByNameAsync(user.UserName);
            return await userManager.GetRolesAsync(thisUser);
        }

        public async Task<bool> IsUserAdmin(KSUser user)
        {
            return await userManager.IsInRoleAsync(user, "admin");
        }
    }
}
