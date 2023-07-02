using CMS_Core.Entities;
using CMS_Core.Enums;
using CMS_Infrastructure.AppContext;
using CMS_Infrastructure.IBusiness;
using CMS_Infrastructure.Respone;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CMS_Infrastructure.Business
{
    public class DaoTrangService : IDaoTrangService
    {
        private readonly AppDbContext dbContext;
        public DaoTrangService()
        {
            dbContext = new AppDbContext();
        }



        #region DRAFT
        //public List<dynamic> TaoChuTri()
        //{
        //    var chuTri = new List<PhatTu>();
        //    for (int i = 10; i <= 17; i++)
        //    {
        //        chuTri.Add(dbContext.PhatTu.FirstOrDefault(x => x.Id == i));
        //    }
        //    var res = chuTri.Select(x => new { Id = x.Id, Ten = x.Ho + " " + x.TenDem + " " + x.Ten }).ToList<dynamic>();
        //    return res;
        //}
        public class FilterRequest
        {
            public string? Ten { get; set; }
            public string? ThoiGian { get; set; }
            public bool? DaKetThuc { get; set; }
        }

        #endregion


        public IQueryable<DaoTrang> Filter(FilterRequest filter)
        {
            var query = dbContext.DaoTrang
                          //.Include(x => x.Chua)
                          // .Include(x => x.KieuThanhVien)
                          .OrderBy(x => x.ThoiGianToChuc)
                          .AsQueryable();

            if (!string.IsNullOrEmpty(filter.Ten))
            {
                var searchValue = Regex.Replace(filter.Ten, @"\s+", " ", RegexOptions.IgnoreCase);
                query = query.Where(x => x.NoiToChuc.ToLower().Contains(searchValue));
            }

            if (!string.IsNullOrEmpty(filter.ThoiGian))
            {
                DateTime thoiGianToChuc;
                if (DateTime.TryParseExact(filter.ThoiGian, "yyyy/MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out thoiGianToChuc))
                {
                    query = query.Where(x => x.ThoiGianToChuc.HasValue && x.ThoiGianToChuc.Value.Month == thoiGianToChuc.Month && x.ThoiGianToChuc.Value.Year == thoiGianToChuc.Year);
                }
            }

            if (filter.DaKetThuc.HasValue)
            {
                query = query.Where(x => x.DaKetThuc == filter.DaKetThuc);
            }

            return query;
        }




        //public IQueryable<DaoTrang> Filter(string? ten, DateTime? thoiGian, bool? daKetThuc)
        //{
        //    var query = dbContext.DaoTrang
        //                  //.Include(x => x.Chua)
        //                  // .Include(x => x.KieuThanhVien)
        //                  .OrderBy(x => x.ThoiGianToChuc)
        //                  .AsQueryable();

        //    if (!string.IsNullOrEmpty(ten))
        //    {
        //        var searchValue = Regex.Replace((ten), @"\s+", " ").ToLower();
        //        query = query.Where(x => x.NoiToChuc.ToLower().Contains(searchValue));
        //    }

        //    if (thoiGian.HasValue)
        //    {
        //        query = query.Where(x => x.ThoiGianToChuc.Value.Month == thoiGian.Value.Month && x.ThoiGianToChuc.Value.Year == thoiGian.Value.Year);

        //    }

        //    if (daKetThuc.HasValue)
        //    {
        //        query = query.Where(x => x.DaKetThuc == daKetThuc);
        //    }

        //    return query;
        //}

        public ResponeModel ThemDaoTrang(DaoTrang daoTrang)
        {
            using (var trans = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var daoTrangMoi = dbContext.DaoTrang.FirstOrDefault(x => x.Id == daoTrang.Id);
                    if (daoTrangMoi != null)
                        return new ResponeModel
                        {
                            Message = "Đạo tràng đã tồn tại!",
                            IsSuccess = false
                        };

                    dbContext.Add(daoTrang);
                    dbContext.SaveChanges();
                    trans.Commit();
                    return new ResponeModel
                    {
                        Message = "Thêm đạo tràng thành công!",
                        IsSuccess = true,

                    };
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return new ResponeModel
                    {
                        Message = "Lỗi trong quá trình thêm đạo tràng!",
                        IsSuccess = true
                    };
                }
            }
        }

        public ResponeModel SuaDaoTrang(DaoTrang daoTrang)
        {
            using (var trans = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var daoTrangCanSua = dbContext.DaoTrang.Find(daoTrang.Id);
                    if (daoTrangCanSua == null)
                        return new ResponeModel
                        {
                            Message = "Đạo tràng không tồn tại!",
                            IsSuccess = false
                        };

                    daoTrangCanSua.NoiToChuc = daoTrang.NoiToChuc;
                    daoTrangCanSua.SoThanhVienThamGia = daoTrang.SoThanhVienThamGia;
                    daoTrangCanSua.ThoiGianToChuc = daoTrang.ThoiGianToChuc;
                    daoTrangCanSua.NoiDung = daoTrang.NoiDung;
                    daoTrangCanSua.DaKetThuc = daoTrang.DaKetThuc;

                    dbContext.DaoTrang.Update(daoTrangCanSua);
                    dbContext.SaveChanges();
                    trans.Commit();
                    return new ResponeModel
                    {
                        Message = "Sửa thông tin thành công!",
                        IsSuccess = true,
                        //Data = TaoChuTri()
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

        public ResponeModel XoaDaoTrang(int daoTrangId)
        {
            var daoTrang = dbContext.DaoTrang.FirstOrDefault(x => x.Id == daoTrangId);
            using (var trans = dbContext.Database.BeginTransaction())
            {
                try
                {
                    if (daoTrang == null)
                        return new ResponeModel
                        {
                            Message = "Đạo tràng cần xoá không tồn tại!",
                            IsSuccess = false
                        };


                    var lst_PhatTuDaoTrang = dbContext.PhatTuDaoTrang.Where(x => x.DaoTrangId == daoTrangId);
                    var lst_DonDangKy = dbContext.DonDangKy.Where(x => x.DaoTrangId == daoTrangId);
                    dbContext.Remove(daoTrang);
                    dbContext.RemoveRange(lst_DonDangKy);
                    dbContext.RemoveRange(lst_PhatTuDaoTrang);
                    dbContext.SaveChanges();
                    trans.Commit();
                    return new ResponeModel
                    {
                        Message = "Xoá đạo tràng thành công!",
                        IsSuccess = true
                    };
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return new ResponeModel
                    {
                        Message = "Lỗi trong quá trình xoá đạo tràng!",
                        IsSuccess = false
                    };
                }
            }
        }

    }
}