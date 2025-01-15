using PracticeInternship.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInternship.Domain.Entities
{
    public class XNK_Nhap_Kho : BaseEntities
    {
        // khi thay đổi hiệu chỉnh phần header là table DM_Nhap_Kho thì lưu thông tin vào table này
        [ForeignKey("DM_Nhap_Kho")]
        public Guid Nhap_Kho_Id { get; set; }

        public string? So_Phieu_Nhap_Kho { get; set; }

        public Guid Kho_Id { get; set; }

        public Guid NCC_Id { get; set; }

        public DateTime Ngay_Nhap_kho { get; set; }

        public string? Ghi_Chu { get; set; }

        public virtual DM_Nhap_Kho DM_Nhap_Kho { get; set; } = null!;
    }
}
