using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInternship.Domain.Entities
{
    public class DM_San_Pham
    {
        public int Id { get; set; } 
        public string? Ma_San_Pham { get; set; }
        public string? Ten_San_Pham { get; set; }
        public string? Ghi_Chu { get; set; }
        
        // relationship
        public int Loai_San_Pham_Id { get; set; }
        public int Don_Vi_Tinh_Id { get; set; }
    }
}
