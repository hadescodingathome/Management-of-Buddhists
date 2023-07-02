using CMS_Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Entities
{
    public class PhatTu : IdentityUser
    {
        public string Ho { get; set; }
        public string TenDem { get; set; }
        public string Ten { get; set; }
        public string? PhapDanh { get; set; }
        public byte[]? AnhChup { get; set; }
        public string SoDienThoai { get; set; }
        public DateTime NgaySinh { get; set; }
        public DateTime? NgayXuatGia { get; set; }
        public bool DaHoanTuc { get; set; }
        public DateTime? NgayHoanTuc { get; set; }
        public GioiTinh GioiTinh { get; set; }
        public int? KieuThanhVienId { get; set; }
        public KieuThanhVien? KieuThanhVien { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public int? ChuaId { get; set; }
        public Chua? Chua { get; set; }
        public int? SoBuoiThamGia { get; set; }

    }
}
