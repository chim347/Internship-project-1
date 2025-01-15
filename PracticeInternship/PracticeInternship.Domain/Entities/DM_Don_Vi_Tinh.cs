using PracticeInternship.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInternship.Domain.Entities
{
    public class DM_Don_Vi_Tinh : BaseEntities
    {
        public string? Ten_Don_Vi_Tinh { get; set; }
        public string? Ghi_Chu {  get; set; }

        // relationship
        public virtual IList<DM_San_Pham> DM_San_Pham { get; set; } = null!;
    }
}
