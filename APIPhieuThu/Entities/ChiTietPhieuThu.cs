namespace APIPhieuThu.Entities
{
    public class ChiTietPhieuThu
    {
        public int ChiTietPhieuThuId { get; set; }
        public int NguyenLieuId { get; set; }
        public int? PhieuThuId { get; set; }
        public int SoLuongBan { get; set; }
        public PhieuThu? PhieuThu { get; set; }
        public NguyenLieu? NguyenLieu { get; set; }  

    }
}
