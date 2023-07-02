using CMS_Core.LogFun;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CMS_WebDesignCore.LogFun
{
    public class LoginRespone
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string? Token { get; set; }
        public IdentityUser data { get; set; }
    }
}
