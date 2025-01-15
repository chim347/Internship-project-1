using PracticeInternship.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInternship.Domain.Entities
{
    public class DM_San_Pham : BaseEntities
    {
        public string? Ma_San_Pham { get; set; }
        public string? Ten_San_Pham { get; set; }
        public string? Ghi_Chu { get; set; }

        [ForeignKey("DM_Loai_San_Pham")]
        public Guid Loai_San_Pham_Id { get; set; }

        [ForeignKey("DM_Don_Vi_Tinh")]
        public Guid Don_Vi_Tinh_Id { get; set; }

        // relationship
        public virtual IList<DM_Nhap_Kho_Raw_Data> DM_Nhap_Kho_Raw_Data { get; set; } = null!;
        public virtual DM_Don_Vi_Tinh DM_Don_Vi_Tinh { get; set; } = null!;
        public virtual DM_Loai_San_Pham DM_Loai_San_Pham { get; set; } = null!;
    }
}
