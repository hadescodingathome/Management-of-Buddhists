using CMS_Core.Business;
using CMS_Core.Entities;
using CMS_WebDesignCore.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CMS_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private IForm _authService;

        public FormController(IForm authService)
        {
            _authService = authService;
        }

        [HttpPost("taodondangky")]
        public IActionResult TaoDonDangKy(DonDangKy donDangKy)
        {
            var res = _authService.TaoDonDangKy(donDangKy);
            return Ok(res);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("xulydon"),Authorize(Roles = "ADMIN")]
        public IActionResult XuliDonDangKi(DonDangKy donDangKy)
        {
            var res = _authService.XuliDonDangKi(donDangKy);
            return Ok(res);
        }
    }
}
