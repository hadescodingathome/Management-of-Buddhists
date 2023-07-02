using CMS_Core.Business;
using CMS_Core.Entities;
using CMS_Core.Enums;
using CMS_Infrastructure.AppContext;
using CMS_Infrastructure.IBusiness;
using CMS_Infrastructure.Respone;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CMS_Infrastructure.Business
{
    public class PhatTuServices : IPhatTuServices
    {
        private readonly AppDbContext dbContext = new AppDbContext();
        //public IQueryable<PhatTu> Filter(string? ten, string? phapDanh, string? email, bool? daHoanTuc, GioiTinh? gioiTinh)
        //{
        //    var query = dbContext.PhatTu
        //        // .Include(x => x.Chua)
        //        // .Include(x => x.KieuThanhVien)
        //        .OrderBy(x => x.Ten)
        //        .AsQueryable();

        //    if (!string.IsNullOrEmpty(ten))
        //    {
        //        var searchValue = Regex.Replace((ten), @"\s+", " ").ToLower();
        //        query = query.Where(x => x.Ten.ToLower().Contains(searchValue)
        //                                || x.TenDem.ToLower().Contains(searchValue)
        //                                || x.Ho.ToLower().Contains(searchValue)
        //                                || (x.Ho + " " + x.TenDem + " " + x.Ten).ToLower().Contains(searchValue));
        //    }


        //    if (!string.IsNullOrEmpty(phapDanh))
        //    {
        //        query = query.Where(x => x.PhapDanh.ToLower().Contains(phapDanh.ToLower()));
        //    }

        //    if (!string.IsNullOrEmpty(email))
        //    {
        //        query = query.Where(x => x.Email.ToLower().Contains(email.ToLower()));
        //    }

        //    if (daHoanTuc.HasValue)
        //    {
        //        query = query.Where(x => x.DaHoanTuc == daHoanTuc);
        //    }

        //    if (gioiTinh.HasValue)
        //    {
        //        query = query.Where(x => x.GioiTinh == gioiTinh);
        //    }
        //    var filteredPhatTus = query.ToList(); // Execute the query and materialize the results

        //    foreach (var pt in filteredPhatTus)
        //    {
        //        pt.SoBuoiThamGia = dbContext.PhatTuDaoTrang
        //            .Count(ptdt => ptdt.PhatTuId == pt.Id && ptdt.DaThamGia == true);
        //    }

        //    return filteredPhatTus.AsQueryable();
        //}


        public ResponeModel ThemPhatTu(PhatTu phatTuMoi)
        {
            using (var trans = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var existingPhatTu = dbContext.PhatTu.FirstOrDefault(x => x.Id == phatTuMoi.Id);

                    if (existingPhatTu != null)
                    {
                        return new ResponeModel
                        {
                            Message = "Phật tử đã tồn tại!",
                            IsSuccess = false
                        };
                    }

                    dbContext.Add(phatTuMoi);
                    dbContext.SaveChanges();
                    trans.Commit();

                    return new ResponeModel
                    {
                        Message = "Thêm phật tử mới thành công!",
                        IsSuccess = true
                    };
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return new ResponeModel
                    {
                        Message = "Lỗi trong quá trình thêm phật tử!",
                        IsSuccess = false
                    };
                }
            }
        }


        public ResponeModel SuaThongTin(PhatTu phatTuMoi)
        {
            using (var trans = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var phatTuCanSua = dbContext.PhatTu.Find(phatTuMoi.Id);
                    if (phatTuCanSua == null)
                        return new ResponeModel
                        {
                            Message = "Phật tử không tồn tại!",
                            IsSuccess = false
                        };

                    phatTuCanSua.Ho = phatTuMoi.Ho;
                    phatTuCanSua.TenDem = phatTuMoi.TenDem;
                    phatTuCanSua.Ten = phatTuMoi.Ten;
                    phatTuCanSua.AnhChup = phatTuMoi.AnhChup;
                    phatTuCanSua.NgaySinh = phatTuMoi.NgaySinh;
                    phatTuCanSua.NgayHoanTuc = phatTuMoi.NgayHoanTuc;
                    phatTuCanSua.DaHoanTuc = phatTuMoi.DaHoanTuc;
                    phatTuCanSua.SoDienThoai = phatTuMoi.SoDienThoai;
                    phatTuCanSua.NgayXuatGia = phatTuMoi.NgayXuatGia;
                    phatTuCanSua.NgayCapNhat = DateTime.Now.Date;
                    phatTuCanSua.KieuThanhVienId = phatTuMoi.KieuThanhVienId;
                    phatTuCanSua.GioiTinh = phatTuMoi.GioiTinh;
                    dbContext.PhatTu.Update(phatTuCanSua);
                    dbContext.SaveChanges();
                    trans.Commit();
                    return new ResponeModel
                    {
                        Message = "Sửa thông tin thành công!",
                        IsSuccess = true
                    };
                }

                catch (Exception ex)
                {
                    trans.Rollback();
                    return new ResponeModel
                    {
                        Message = "Lỗi trong quá trình sửa thông tin!",
                        IsSuccess = false
                    };
                }
            }
        }


        public ResponeModel XoaPhatTu(int phatTuId)
        {
            using (var trans = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var phatTuCanXoa = dbContext.PhatTu.Find(phatTuId);
                    if (phatTuCanXoa == null)
                        return new ResponeModel
                        {
                            Message = "Phật tử không tồn tại!",
                            IsSuccess = false
                        };
                    var phatTuDaoTrang = dbContext.PhatTuDaoTrang.Where(x => x.PhatTuId == phatTuId);
                    var donDangKy = dbContext.DonDangKy.Where(x => x.PhatTuId == phatTuId);
                    dbContext.RemoveRange(phatTuDaoTrang);
                    dbContext.RemoveRange(donDangKy);
                    dbContext.PhatTu.Remove(phatTuCanXoa);
                    dbContext.SaveChanges();
                    return new ResponeModel
                    {
                        Message = "Xoá phật tử thành công!",
                        IsSuccess = true
                    };
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return new ResponeModel
                    {
                        Message = "Lỗi trong quá trình xoá phật tử!",
                        IsSuccess = false
                    };
                }
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
