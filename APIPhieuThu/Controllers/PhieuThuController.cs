using APIPhieuThu.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIPhieuThu.Services;
using APIPhieuThu.Entities;

namespace APIPhieuThu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuThuController : ControllerBase
    {
        private readonly PhieuThuIServices service;

        public PhieuThuController()
        {
            service = new PhieuThuServices();
        }
        //[HttpPost]
        //public IActionResult ThemNguyenLieu([FromBody] NguyenLieu nguyenLieu)
        //{
        //    service.ThemNguyenLieu(nguyenLieu);
        //    return Ok();
        //}
        //[HttpPost]
        //public IActionResult ThemChiTietPhieuThu([FromBody] ChiTietPhieuThu chitiet)
        //{
        //    service.ThemChiTietPhieuThu(chitiet);
        //    return Ok();
        //}
        [HttpPost]
        public IActionResult ThemPhieuThu([FromBody] PhieuThu phieuThu)
        {
            service.ThemPhieuThu(phieuThu);
            return Ok();
        }
        [HttpPut]
        public IActionResult SuaPhieuThu([FromBody] PhieuThu phieuThu)
        {
            service.SuaPhieuThu(phieuThu);
            return Ok();
        }
        //[HttpDelete]
        //public IActionResult XoaPhieuThu([FromQuery] int phieuThuId)
        //{
        //    service.XoaPhieuThu(phieuThuId);
        //    return Ok();
        //}
        //[HttpGet]
        //public IActionResult LayDsPhieuThu([FromQuery] int? month)
        //{
        //    var layds = service.LayDsPhieuThu(month);
        //    return Ok(layds);
        //}
    }
}
