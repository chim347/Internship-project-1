using PracticeInternship.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInternship.Domain.Entities
{
    public class XNK_Xuat_Kho : BaseEntities
    {
        [ForeignKey("DM_Xuat_Kho")]
        public Guid Xuat_Kho_Id { get; set; }

        public string? So_Phieu_Xuat_Kho { get; set; }

        public Guid Kho_Id { get; set; }

        public DateTime Ngay_Xuat_kho { get; set; }

        public string? Ghi_Chu { get; set; }

        public virtual DM_Xuat_Kho DM_Xuat_Kho { get; set; } = null!;
    }
}
