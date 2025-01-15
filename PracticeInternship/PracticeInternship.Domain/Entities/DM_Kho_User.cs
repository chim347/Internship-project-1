using PracticeInternship.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInternship.Domain.Entities
{
    public class DM_Kho_User : BaseEntities
    {
        public string? Ma_Dang_Nhap {  get; set; }

        [ForeignKey("DM_Kho")]
        public Guid Kho_Id { get; set; }

        // relationship
        public virtual DM_Kho DM_Kho { get; set; } = null!;
    }
}
