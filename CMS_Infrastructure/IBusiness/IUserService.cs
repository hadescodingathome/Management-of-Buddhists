using CMS_Core.LogFun;
using CMS_WebDesignCore.LogFun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Business
{
    public interface IUserService
    {
        Task<RegisterRespone> RegisterUserAsync(RegisterViewModel model);
        Task<LoginRespone> LoginUserAsync(LoginViewModel model);
    }
}
