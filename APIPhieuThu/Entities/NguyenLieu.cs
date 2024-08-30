using System.Reflection.Metadata.Ecma335;

namespace APIPhieuThu.Entities
{
    public class NguyenLieu
    {
        public int NguyenLieuId { get; set; }
        public int LoaiNguyenLieuId { get; set; }
        public string TenNguyenLieu{ get; set; }
        public int GiaBan { get; set; }
        public string DVT { get; set; }
        public int SoLuongKho { get; set; }
        public LoaiNguyenLieu? LoaiNguyenLieu { get; set; }
        List<ChiTietPhieuThu> chiTietPhieuThus { get; set; }
    }
}
