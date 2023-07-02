using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Entities
{
    public class Chua : BaseEntity
    {
        public string TenChua { get; set; }
        public DateTime NgayThanhLap { get; set; }
        public string DiaChi { get; set; }
        public string TruTri { get; set; }
        public DateTime? CapNhat { get; set; }
    }
}
