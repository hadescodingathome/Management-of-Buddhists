using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;  
using System.Text;
using System.Threading.Tasks;

namespace CMS_Core.Entities
{
    public class PhatTuDaoTrang : BaseEntity
    {
      
        public int? PhatTuId { get; set; }
        public virtual PhatTu? PhatTu { get; set; }
        public int DaoTrangId { get; set; }
        public DaoTrang? DaoTrang { get; set; }
        public bool DaThamGia { get; set; }
        public string? LyDoKhongThamGia { get; set; }
    }
}
