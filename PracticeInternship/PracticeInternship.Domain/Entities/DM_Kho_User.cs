using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInternship.Domain.Entities
{
    public class DM_Kho_User
    {
        public int Id { get; set; }
        public string? Ma_Dang_Nhap {  get; set; }
        public int Kho_Id { get; set; }
    }
}
