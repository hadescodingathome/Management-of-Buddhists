using CMS_Core.Business;
using CMS_Core.Entities;
using CMS_Core.Enums;
using CMS_Core.Helper;
using CMS_Infrastructure.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace CMS_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhatTuController : ControllerBase
    {
        private PhatTuServices phatTuServices;
        public PhatTuController()
        {
            phatTuServices = new PhatTuServices();
        }

        // api/phattu/laydanhsach
        //[Authorize(AuthenticationSchemes = "Bearer")]
        //[HttpGet("laydanhsach")]
        //public IActionResult Filter([FromQuery] string? ten,
        //                            [FromQuery] string? phapDanh,
        //                            [FromQuery] string? email,
        //                            [FromQuery] bool? daHoanTuc = null,
        //                            [FromQuery] GioiTinh? gioiTinh = null,
        //                            [FromQuery] Pagination pagination = null)
        //{
        //    var query = phatTuServices.Filter(ten, phapDanh, email, daHoanTuc, gioiTinh);
        //    var phatTus = PageResult<PhatTu>.ToPageResult(pagination, query).AsEnumerable();
        //    pagination.TotalCount = query.Count();
        //    var res = new PageResult<PhatTu>(pagination, phatTus);
        //    return Ok(res);
        //}

        [HttpPut("suathongtin")]
        public IActionResult SuaThongTin(PhatTu phatTu)
        {
            var res = phatTuServices.SuaThongTin(phatTu);
            return Ok(res);
        }

        [HttpPost("themphatu")]
        public IActionResult ThemPhatTu(PhatTu phatTu)
        {
            var res = phatTuServices.ThemPhatTu(phatTu);
            return Ok(res);
        }

        [HttpDelete("xoaphattu")]
        public IActionResult XoaPhatTu(int phatTuId)
        {
            var res = phatTuServices.XoaPhatTu(phatTuId);
            return Ok(res);
        }
    }
}
