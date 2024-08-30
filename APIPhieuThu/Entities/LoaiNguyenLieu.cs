using System.Security.Principal;

namespace APIPhieuThu.Entities
{
    public class LoaiNguyenLieu
    {
        public int LoaiNguyenLieuId { get; set; }
        public string TenLoai { get; set; }
        public string MoTa { get; set; }
        List<NguyenLieu> nguyenLieus { get; set; }
    }
}
