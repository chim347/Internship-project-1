using PracticeInternship.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInternship.Domain.Entities
{
    public class DM_Xuat_Kho : BaseEntities
    {
        public string? So_Phieu_Xuat_Kho {  get; set; }

        [ForeignKey("DM_Kho")]
        public Guid Kho_Id { get; set; }
        public DateTime Ngay_Xuat_Kho { get; set; }
        public string? Ghi_Chu {  get; set; }

        public virtual DM_Kho DM_Kho { get; set; } = null!;
        public virtual IList<DM_Xuat_Kho_Raw_Data> DM_Xuat_Kho_Raw_Data { get; set; } = null!;
    }
}
