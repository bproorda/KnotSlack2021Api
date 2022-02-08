
using knotslack2022api.Models.Identity;
using Knotslack2022api.Data;
using Microsoft.AspNetCore.Identity;

namespace knotslack2022api.Services
{ 
    public class UserManager : IUserManager
    {
        private readonly UserManager<KSUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IConfiguration _configuration;
        //private KnotSlack2022DbContext _context;

        public UserManager(
            UserManager<KSUser> userManager
            //RoleManager<IdentityRole> roleManager,
            //IConfiguration configuration,
            //KnotSlack2022DbContext context
            )
        {
            _userManager = userManager;
            //_roleManager = roleManager;
            //_configuration = configuration;
            //_context = context;
        }

        public string Testing()
        {
            return "Is this working?";
        }
    }
}
