using CMS_Core.Entities;
using CMS_Infrastructure.Respone;
using static CMS_Infrastructure.Business.DaoTrangService;

namespace CMS_Infrastructure.IBusiness
{
    public interface IDaoTrangService
    {
        IQueryable<DaoTrang> Filter(FilterRequest filter);
        ResponeModel SuaDaoTrang(DaoTrang daoTrang);
        ResponeModel ThemDaoTrang(DaoTrang daoTrang);
        ResponeModel XoaDaoTrang(int daoTrangId);
    }
}