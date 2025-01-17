using PracticeInternship.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInternship.Domain.Entities
{
    public class DM_Xuat_Kho_Raw_Data : BaseEntities
    {
        [ForeignKey("DM_Xuat_Kho")]
        public Guid Xuat_Kho_Id { get; set; }
        [ForeignKey("DM_San_Pham")]
        public Guid San_Pham_Id {  get; set; }

        public int SL_Xuat {  get; set; }
        public decimal Don_Gia_Xuat { get; set; }

        public virtual DM_Xuat_Kho DM_Xuat_Kho { get; set; } = null!;
        public virtual DM_San_Pham DM_San_Pham { get; set; } = null!;
    }
}
