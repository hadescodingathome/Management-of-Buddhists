using CMS_Core.Entities;
using CMS_Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_WebDesignCore.Business
{
    public interface IForm
    {
        public DonDangKy TaoDonDangKy(DonDangKy donDangKy);
        public DonDangKy XuliDonDangKi(DonDangKy donDangKy);
    }
}
