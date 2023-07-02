using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Entities
{
    public class KieuThanhVien : BaseEntity
    {
        public string Code { get; set; }
        public string TenKieu { get; set; }
    }
}
