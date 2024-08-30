using APIPhieuThu.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace APIPhieuThu.IServices
{
    public interface PhieuThuIServices
    {
        public void ThemNguyenLieu(NguyenLieu nguyenLieu);
        public void ThemChiTietPhieuThu(ChiTietPhieuThu chiTietPhieuThu);
        public void ThemPhieuThu(PhieuThu phieuThu);
        public void SuaPhieuThu(PhieuThu phieuThu);
        public void XoaPhieuThu(int phieuThuId);
        public IEnumerable<PhieuThu> LayDsPhieuThu(int? month);
    }
}
