using PracticeInternship.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeInternship.Domain.Entities
{
    public class DM_Nhap_Kho_Raw_Data : BaseEntities
    {
        [ForeignKey("DM_Nhap_Kho")]
        public Guid Nhap_Kho_Id { get; set; }

        [ForeignKey("DM_San_Pham")]
        public Guid San_Pham_Id { get; set; }

        public int SL_Nhap { get; set; }

        public decimal Don_Gia_Nhap { get; set; }

        // relationship
        public virtual DM_Nhap_Kho DM_Nhap_Kho { get; set; } = null!;
        public virtual DM_San_Pham DM_San_Pham { get; set; } = null!;
    }
}
