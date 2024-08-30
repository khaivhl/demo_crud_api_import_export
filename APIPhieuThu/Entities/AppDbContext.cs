using Microsoft.EntityFrameworkCore;
namespace APIPhieuThu.Entities
{
    public class AppDbContext:DbContext
    {
        public virtual DbSet<PhieuThu> PhieuThu { get; set; }
        public virtual DbSet<ChiTietPhieuThu> ChiTietPhieuThu { get; set; }
        public virtual DbSet<NguyenLieu> NguyenLieu { get; set; }
        public virtual DbSet<LoaiNguyenLieu> LoaiNguyenLieu { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server = DESKTOP-AB0SO82; Database = APIPhieuThu   ; Trusted_Connection = True;TrustServerCertificate=True;MultipleActiveResultSets=True");
        }
    }
}
