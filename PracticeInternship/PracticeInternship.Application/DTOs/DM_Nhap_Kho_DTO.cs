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
    public class DM_Nhap_Kho_Raw_Data_DTO_Version_2
    {
        public string? Nhap_Kho_Id { get; set; }

        public string? Ma_San_Pham { get; set; }
        public string? Ten_San_Pham { get; set; }

        public int SL_Nhap { get; set; }

        public decimal Don_Gia_Nhap { get; set; }

        public string? Loai_San_Pham_Id { get; set; }
        public string? Don_Vi_Tinh_Id { get; set; }
    }
    public class DM_Nhap_Kho_Raw_Data_DTO_Edit
    {
        public string? Id { get; set; }
        public int SL_Nhap { get; set; }
        public decimal Don_Gia_Nhap { get; set; }
    }

    public class DM_Nhap_Kho_Detail_Response
    {
        public string? Id { get; set; }

        public string? So_Phieu_Nhap_Kho { get; set; }

        public string? Kho_Id { get; set; }
        public string? Ten_Kho { get; set; }

        public string? NCC_Id { get; set; }

        public DateTime Ngay_Nhap_kho { get; set; }

        public string? Ghi_Chu { get; set; }

        public ICollection<DM_Nhap_Kho_Raw_Data_Response>? List_DM_Nhap_Kho_Raw_Data_Response { get; set; }
    }

    public class DM_Nhap_Kho_Raw_Data_Response
    {
        public string? Id { get; set; }

        public string? Nhap_Kho_Id { get; set; }

        public string? San_Pham_Id { get; set; }
        public string? Ma_San_Pham { get; set; }
        public string? Ten_San_Pham { get; set; }

        public int SL_Nhap { get; set; }

        public decimal Don_Gia_Nhap { get; set; }

        public string? Ten_Don_Vi_Tinh { get; set; }

    }
}
