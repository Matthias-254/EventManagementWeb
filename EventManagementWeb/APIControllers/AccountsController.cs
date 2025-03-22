using EventManagementWeb.APIModels;
using EventManagementWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementWeb.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly SignInManager<EventManagementUser> _signInManager;

        public AccountsController(SignInManager<EventManagementUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPut]
        //[Route("/api/login")]
        public async Task<ActionResult<Boolean>> Login([FromBody] LoginModel login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Name, login.Password, true, lockoutOnFailure: false);

            return result.Succeeded;
        }

    }
}
