namespace APIPhieuThu.Entities
{
    public class PhieuThu
    {
        public int PhieuThuId { get; set; }
        public DateTime NgayLap { get; set; }
        public string NhanVienLap { get; set; }
        public string GhiChu { get; set; }
        public double? ThanhTien { get; set; }
        public IEnumerable<ChiTietPhieuThu> chiTietPhieuThus { get; set; }
    }
}
