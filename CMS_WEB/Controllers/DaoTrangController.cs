using CMS_Core.Entities;
using CMS_Core.Helper;
using CMS_Infrastructure.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CMS_Infrastructure.Business.DaoTrangService;

namespace CMS_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaoTrangController : ControllerBase
    {
        private DaoTrangService daoTrangService;
        public DaoTrangController()
        {
            daoTrangService = new DaoTrangService();
        }

        // api/phattu/laydanhsach
        //[Authorize(AuthenticationSchemes = "Bearer")]
        //[HttpGet("laydanhsach")]
        //public IActionResult Filter([FromQuery] string? ten,
        //                            [FromQuery] DateTime? thoiGian,
        //                            [FromQuery] bool? daKetThuc = null,
        //                            [FromQuery] Pagination pagination = null)
        //{
        //    var query = daoTrangService.Filter(ten, thoiGian, daKetThuc);
        //    var daoTrangs = PageResult<DaoTrang>.ToPageResult(pagination, query).AsEnumerable();
        //    pagination.TotalCount = query.Count();
        //    var res = new PageResult<DaoTrang>(pagination, daoTrangs);
        //    return Ok(res);
        //}

        [HttpPost("laydanhsach")]
        public IActionResult Filter([FromBody] FilterRequest filter,
                                        [FromQuery] Pagination pagination = null)
        {
            var query = daoTrangService.Filter(filter);
            var daoTrangs = PageResult<DaoTrang>.ToPageResult(pagination, query).AsEnumerable();
            pagination.TotalCount = query.Count();
            var res = new PageResult<DaoTrang>(pagination, daoTrangs);
            return Ok(res);
        }


        [HttpPut("suathongtin")]
        public IActionResult SuaThongTin(DaoTrang daoTrang)
        {
            var res = daoTrangService.SuaDaoTrang(daoTrang);
            return Ok(res);
        }

        [HttpPost("themdaotrang")]
        public IActionResult ThemDaoTrang(DaoTrang daoTrang)
        {
            var res = daoTrangService.ThemDaoTrang(daoTrang);
            return Ok(res);
        }

        [HttpDelete("xoadaotrang")]
        public IActionResult XoaDaoTrang(int daotrangId)
        {
            var res = daoTrangService.XoaDaoTrang(daotrangId);
            return Ok(res);
        }

        //[HttpGet("danhsachchutri")]
        //public IActionResult DanhSachChuTri()
        //{
        //    var res = daoTrangService.TaoChuTri();
        //    return Ok(res);
        //}

    }
}
