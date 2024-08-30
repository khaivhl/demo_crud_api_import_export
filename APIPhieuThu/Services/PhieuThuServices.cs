using APIPhieuThu.Entities;
using APIPhieuThu.IServices;
using System.Data.Common;

namespace APIPhieuThu.Services
{
    public class PhieuThuServices : PhieuThuIServices
    {
        private readonly AppDbContext dbContext;

        public PhieuThuServices()
        {
            dbContext= new AppDbContext();
        }

        public IEnumerable<PhieuThu> LayDsPhieuThu(int? month)
        {
            var ds = dbContext.PhieuThu.OrderBy(x => x.NgayLap).AsQueryable();
            if (month.HasValue)
            {
                ds = ds.Where(x => x.NgayLap.Month == month);
            }
            return ds;
        }

        public void SuaPhieuThu(PhieuThu phieuThu)
        {
            using (var tran = dbContext.Database.BeginTransaction())
            {
                if (dbContext.PhieuThu.Any(x => x.PhieuThuId==phieuThu.PhieuThuId))
                {
       
                    if (phieuThu.chiTietPhieuThus == null || phieuThu.chiTietPhieuThus.Count() == 0)
                    {
                        var lstchitiethientai = dbContext.ChiTietPhieuThu.Where(x => x.PhieuThuId == phieuThu.PhieuThuId);
                        dbContext.RemoveRange(lstchitiethientai);
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        var lstchitiethientai = dbContext.ChiTietPhieuThu.Where(x => x.PhieuThuId == phieuThu.PhieuThuId);
                        var lstdelete = new List<ChiTietPhieuThu>();
                        foreach (var chitiet in lstchitiethientai)
                        {
                            if (!phieuThu.chiTietPhieuThus.Any(x => x.ChiTietPhieuThuId==chitiet.ChiTietPhieuThuId))
                            {   
                                lstdelete.Add(chitiet);
                            }
                            else
                            {
                                var chitietmoi = phieuThu.chiTietPhieuThus.FirstOrDefault(x => x.ChiTietPhieuThuId == chitiet.ChiTietPhieuThuId);
                                chitiet.NguyenLieuId = chitietmoi.NguyenLieuId;
                                chitiet.SoLuongBan = chitietmoi.NguyenLieuId;
                                dbContext.Update(chitiet);

                                var nguyenlieuhientai = dbContext.NguyenLieu.FirstOrDefault(x => x.NguyenLieuId == chitiet.NguyenLieuId);
                                nguyenlieuhientai.SoLuongKho -= chitiet.SoLuongBan;
                                dbContext.Update(nguyenlieuhientai);

                                var phieuthuhientai = dbContext.PhieuThu.FirstOrDefault(x => x.PhieuThuId == chitiet.PhieuThuId);
                                phieuThu.ThanhTien = nguyenlieuhientai.SoLuongKho * chitiet.SoLuongBan;
                                dbContext.Update(phieuThu);
                            }
                        }
                        dbContext.RemoveRange(lstdelete);
                        dbContext.SaveChanges();
                        foreach (var chitiet in phieuThu.chiTietPhieuThus)
                        {
                            if (!lstchitiethientai.Any(x => x.PhieuThuId == chitiet.PhieuThuId))
                            {
                                chitiet.PhieuThuId = phieuThu.PhieuThuId;
                            }
                        }
                    }
                    dbContext.SaveChanges();
                    tran.Commit();
                }
                else
                {
                    tran.Rollback();
                    throw new Exception("Phieu thu khong ton tai");
                }
                
            }
        }

        public void ThemChiTietPhieuThu(ChiTietPhieuThu chiTietPhieuThu)
        {
            using (var tran = dbContext.Database.BeginTransaction())
            {
                if (dbContext.PhieuThu.Any(x=>x.PhieuThuId== chiTietPhieuThu.PhieuThuId))
                {
                    if (dbContext.ChiTietPhieuThu.Any(x => x.NguyenLieuId == chiTietPhieuThu.NguyenLieuId))
                    {
                        var chitiethientai = dbContext.ChiTietPhieuThu.FirstOrDefault(x => x.NguyenLieuId == chiTietPhieuThu.NguyenLieuId);
                        chitiethientai.SoLuongBan = chiTietPhieuThu.SoLuongBan;
                        dbContext.Update(chitiethientai);
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        dbContext.Add(chiTietPhieuThu);
                        dbContext.SaveChanges();
                    }
                    tran.Commit();
                }
                else
                {
                    tran.Rollback();
                    throw new Exception("phieu thu khong ton tai");
                }
            }
        }

        public void ThemNguyenLieu(NguyenLieu nguyenLieu)
        {
            using (var tran = dbContext.Database.BeginTransaction())
            {
                if (dbContext.LoaiNguyenLieu.Any(x=>x.LoaiNguyenLieuId==nguyenLieu.LoaiNguyenLieuId))
                {
                    if (dbContext.NguyenLieu.Any(x=>x.TenNguyenLieu.ToLower()==nguyenLieu.TenNguyenLieu.ToLower()))
                    {
                        var nguyenlieuhientai = dbContext.NguyenLieu.FirstOrDefault(x => x.LoaiNguyenLieuId == nguyenLieu.LoaiNguyenLieuId);
                        nguyenlieuhientai.SoLuongKho += nguyenLieu.SoLuongKho;
                        dbContext.Update(nguyenlieuhientai);
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        var nguyenlieuhientai = dbContext.NguyenLieu.FirstOrDefault(x => x.LoaiNguyenLieuId == nguyenLieu.LoaiNguyenLieuId);
                        dbContext.Add(nguyenLieu);
                        dbContext.SaveChanges();
                    }
                    tran.Commit();
                }
                else
                {
                    tran.Rollback();
                    throw new Exception("Loai nguyen lieu khong ton tai");
                }
            }
        }

        public void ThemPhieuThu(PhieuThu phieuThu)
        {
            using(var tran = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var lstchitietphieuthu = phieuThu.chiTietPhieuThus;
                    phieuThu.chiTietPhieuThus = null;
                    phieuThu.NgayLap = DateTime.Now;
                    dbContext.Add(phieuThu);
                    dbContext.SaveChanges();
                    foreach (var chitiet in lstchitietphieuthu)
                    {
                        if (dbContext.NguyenLieu.Any(x => x.NguyenLieuId == chitiet.NguyenLieuId))
                        {
                            chitiet.PhieuThuId = phieuThu.PhieuThuId;
                            dbContext.Add(chitiet);
                            dbContext.SaveChanges();

                            var nguyenlieu = dbContext.NguyenLieu.FirstOrDefault(x => x.NguyenLieuId == chitiet.NguyenLieuId);
                            nguyenlieu.SoLuongKho -= chitiet.SoLuongBan;
                            dbContext.Update(nguyenlieu); 
                            dbContext.SaveChanges();

                            phieuThu.ThanhTien = nguyenlieu.GiaBan * chitiet.SoLuongBan;
                            dbContext.Update(phieuThu);
                            dbContext.SaveChanges();

                            tran.Commit();
                        }
                        else
                        {
                            tran.Rollback();
                            throw new Exception("nguyen lieu khong ton tai");
                        }
                    }
                }
                catch 
                {
                    tran.Rollback();
                    throw new Exception("them that bai");
                }
                
            }
        }

        public void XoaPhieuThu(int phieuThuId)
        {
            using(var tran = dbContext.Database.BeginTransaction())
            {
                var phieuthuhientai = dbContext.PhieuThu.FirstOrDefault(x => x.PhieuThuId == phieuThuId);
                if (phieuthuhientai!=null)
                {
                    dbContext.Remove(phieuthuhientai);
                    dbContext.SaveChanges();
                    tran.Commit();
                }
                else
                {
                    tran.Rollback();
                    throw new Exception("phieu thu id khong ton tai");
                }
            }
        }
    }
}
