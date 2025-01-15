using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInternship.Application.DTOs
{
    public class DM_Nhap_Kho_DTO
    {
        public string? So_Phieu_Nhap_Kho { get; set; }

        public string? Kho_Id { get; set; }

        public string? NCC_Id { get; set; }

        public DateTime Ngay_Nhap_kho { get; set; }

        public string? Ghi_Chu { get; set; }

        public ICollection<DM_Nhap_Kho_Raw_Data_DTO>? List_DM_Nhap_Kho_Raw_Data_DTO { get; set; }
    }

    public class DM_Nhap_Kho_Raw_Data_DTO
    {
        public string? San_Pham_Id { get; set; }

        public int SL_Nhap { get; set; }

        public decimal Don_Gia_Nhap { get; set; }
    }
}
