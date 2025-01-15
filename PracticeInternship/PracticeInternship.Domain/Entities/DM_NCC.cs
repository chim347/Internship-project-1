using PracticeInternship.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInternship.Domain.Entities
{
    public class DM_NCC : BaseEntities
    {
        public string? Ma_NCC { get; set; }
        public string? Ten_NCC { get; set; }
        public string? Ghi_Chu { get; set; }

        // relationship
        public virtual IList<DM_Nhap_Kho> DM_Nhap_Kho { get; set; } = null!;

    }
}
