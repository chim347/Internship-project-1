using PracticeInternship.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInternship.Domain.Entities
{
    public class DM_Kho : BaseEntities
    {
        public string? Ten_Kho { get; set; }
        public string? Ghi_Chu { get; set; }

        // relationship
        public virtual IList<DM_Kho_User> DM_Kho_User { get; set; } = null!;
        public virtual IList<DM_Nhap_Kho> DM_Nhap_Kho { get; set; } = null!;
    }
}
