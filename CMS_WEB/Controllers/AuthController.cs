using CMS_Core.Business;
using CMS_Core.LogFun;
using CMS_Infrastructure.IBusiness;
using Microsoft.AspNetCore.Mvc;

namespace CMS_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;
        private IMailService _mailService;

        public AuthController(IUserService userService, IMailService mailService)
        {
            _userService = userService;
            _mailService = mailService;
        }

        // api/auth/register
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);

                if (result.IsSuccess)
                    return Ok(result);  //Status code 200

                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid"); //Status code 400
        }


        // api/auth/login
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);

                if (result.IsSuccess)
                {
                    await _mailService.SendEmailAsync(model.Email, "New login", "<h1>Hey! There is a new login to your account noticed.</h1><p>New login to account at " + DateTime.Now + "</p>");
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid");
        }



    }
}
