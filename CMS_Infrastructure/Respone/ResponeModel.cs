using CMS_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Infrastructure.Respone
{
    public class ResponeModel
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public object Data { get; set; }
    }
}
