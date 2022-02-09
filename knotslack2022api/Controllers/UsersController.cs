using knotslack2022api.Models.Identity;
using knotslack2022api.Models.Identity.DTO;
using knotslack2022api.Services;
using Microsoft.AspNetCore.Mvc;

namespace knotslack2022api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            this._userManager = userManager;
        }

        [HttpPost("Register")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterAsync(RegisterData registerData)
        {
            try
            {
                if (registerData.Role == "admin")
                {
                    if (await _userManager.AdminCheckAsync())
                    {
                        return BadRequest(new
                        {
                            message = "registration failed",
                            errors = "Only Admin can assign role of Admin"
                        });
                    }
                }

                var newUser = new KSUser
                {
                    UserName = registerData.UserName,
                    Email = registerData.Email,
                    LoggedIn = true,
                };

                var result = await _userManager.CreateAsync(newUser, registerData.Password, registerData.Role);

                if (!result.Succeeded)
                {
                    return BadRequest(new
                    {
                        message = "registration failed",
                        errors = result.Errors
                    });
                }

                return Ok(await _userManager.CreateUserWithToken(newUser));
            }
            catch
            {
                return View();
            }
        }

    }
}
