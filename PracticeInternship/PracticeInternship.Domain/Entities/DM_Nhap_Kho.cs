using PracticeInternship.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInternship.Domain.Entities
{
    public class DM_Nhap_Kho : BaseEntities
    {
        public string? So_Phieu_Nhap_Kho {  get; set; }

        [ForeignKey("DM_Kho")]
        public Guid Kho_Id { get; set; }

        [ForeignKey("DM_NCC")]
        public Guid NCC_Id { get; set; }

        public DateTime Ngay_Nhap_kho { get; set; }

        public string? Ghi_Chu {  get; set; }

        // relationship
        public virtual IList<DM_Nhap_Kho_Raw_Data> DM_Nhap_Kho_Raw_Data { get; set; } = null!;
        public virtual DM_Kho DM_Kho { get; set; } = null!;
        public virtual DM_NCC DM_NCC { get; set; } = null!;
        public virtual XNK_Nhap_Kho XNK_Nhap_Kho { get; set; } = null!;
    }
}
