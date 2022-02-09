
using knotslack2022api.Models.Identity;
using knotslack2022api.Models.Identity.DTO;
using Knotslack2022api.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace knotslack2022api.Services
{ 
    public class UserManager : IUserManager
    {
        private readonly UserManager<KSUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private KnotSlack2022DbContext _context;

        public UserManager(
            UserManager<KSUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            KnotSlack2022DbContext context
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }

        public Task<bool> CheckPasswordAsync(KSUser user, string password)
        {
            return _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> CreateAsync(KSUser user, string password, string role = "user")
        {
            var result = await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, role);
            //await AddNewUserToGeneral(user.UserName);

            return result;
        }

        public Task<KSUser> FindByIdAsync(string userId)
        {
            return _userManager.FindByIdAsync(userId);
        }

        public Task<KSUser> FindByNameAsync(string username)
        {
            return _userManager.FindByNameAsync(username);
        }

        public Task<IdentityResult> UpdateAsync(KSUser user)
        {
            return _userManager.UpdateAsync(user);
        }

        public async Task<string> CreateToken(KSUser user)
        {
            var secret = _configuration["JWT:Secret"];
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            var signingKey = new SymmetricSecurityKey(secretBytes);
            var roles = (List<string>)await _userManager.GetRolesAsync(user);

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

        public async Task InitializeRolesAsync()
        {
            var userRole = new IdentityRole();
            userRole.Name = "user";
            var adminRole = new IdentityRole();
            adminRole.Name = "admin";
            await _roleManager.CreateAsync(userRole);
            await _roleManager.CreateAsync(adminRole); 
        }

        public async Task<bool> AdminCheckAsync()
        {
            var admins = await _userManager.GetUsersInRoleAsync("admin");

            if (admins.Count() == 0)
            {
                return false;
            } else
            {
                return true;
            }
        }

        public async Task<UserWithToken> CreateUserWithToken(KSUser user)
        {
            return new UserWithToken
            {
                UserId = user.UserName,
                Token = await CreateToken(user),
                //Channels = await GetUserChannels(user),
                //Roles = (List<string>)await GetUserRoles(user),
                LastVisited = DateTime.Now,
            };
        }

        public Task AccessFailedAsync(KSUser user)
        {
            return _userManager.AccessFailedAsync(user);
        }
    }
}
