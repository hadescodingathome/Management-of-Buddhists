using CMS_Core.Entities;
using CMS_Core.Enums;
using CMS_Infrastructure.Respone;

namespace CMS_Infrastructure.IBusiness
{
    public interface IPhatTuServices
    {
        //IQueryable<PhatTu> Filter(string? ten, string? phapDanh, string? email, bool? daHoanTuc, GioiTinh? gioiTinh);
        ResponeModel SuaThongTin(PhatTu phatTuMoi);
        ResponeModel ThemPhatTu(PhatTu phatTuMoi);
        ResponeModel XoaPhatTu(int phatTuId);
    }
}