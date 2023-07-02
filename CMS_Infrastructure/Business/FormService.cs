using CMS_Core.Enums;
using CMS_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS_Infrastructure.AppContext;

namespace CMS_WebDesignCore.Business
{
    public class FormService : IForm
    {
        private readonly AppDbContext db = new AppDbContext();
        public DonDangKy TaoDonDangKy(DonDangKy donDangKy)
        {
            using (var trans = db.Database.BeginTransaction())
            {
                try
                {
                    donDangKy.NgayGuiDon = DateTime.Now.Date;
                    donDangKy.TrangThaiDon = TrangThaiDon.DANGCHO;
                    db.Add(donDangKy);
                    db.SaveChanges();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
            }
            return donDangKy;
        }

        public DonDangKy XuliDonDangKi(DonDangKy donDangKy)
        {
            DonDangKy form = db.DonDangKy.FirstOrDefault(x => x.Id == donDangKy.Id);
            using (var trans = db.Database.BeginTransaction())
            {
                try
                {
                    if (form != null)
                    {
                        form.NgayXuLy = DateTime.Now;
                        form.NguoiXuLyId = donDangKy.NguoiXuLyId;
                        if (donDangKy.TrangThaiDon == TrangThaiDon.DONGY)
                            form.TrangThaiDon = TrangThaiDon.DONGY;
                        else if (donDangKy.TrangThaiDon == TrangThaiDon.KHONGDONGY)
                            form.TrangThaiDon = TrangThaiDon.KHONGDONGY;
                        db.Update(form);
                        db.SaveChanges();
                        trans.Commit(); 
                    }
                    else
                    {
                        throw new NullReferenceException("Form does not exist!");
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
            }
            return form;
        }
    }
}