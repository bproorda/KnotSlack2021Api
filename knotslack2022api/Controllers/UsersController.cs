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

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginData login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);

            if (user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, login.Password);
                if (result)
                {
                    user.LoggedIn = true;
                    await _userManager.UpdateAsync(user);

                    //await chatHub.SendUpdatedUser(user.UserName, user.LoggedIn);

                    return Ok(await _userManager.CreateUserWithToken(user));
                }

                await _userManager.AccessFailedAsync(user);
            }
            return Unauthorized();
        }

        //[Authorize]
        //To update LoggedIn prop in db
        [HttpPost("Logout")]
        public async Task<string> Logout(KSUserDTO userInfo)
        {
            var user = await _userManager.FindByNameAsync(userInfo.UserName);
            if (user != null)
            {
                user.LoggedIn = false;
                await _userManager.UpdateAsync(user);

                //comment out if using postman
                //await chatHub.SendUpdatedUser(user.UserName, user.LoggedIn);
                //await chatHub.UpdateLastVisited(user.UserName);
                return user.UserName;

            }
            return "User Not Found";
        }

    }
}
