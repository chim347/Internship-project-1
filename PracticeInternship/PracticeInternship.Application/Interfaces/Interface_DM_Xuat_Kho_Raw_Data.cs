using PracticeInternship.Application.DTOs;
using PracticeInternship.Application.Responses;
using PracticeInternship.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInternship.Application.Interfaces
{
    public interface Interface_DM_Xuat_Kho_Raw_Data
    {
        Task<Response> CreateAsync(DM_Xuat_Kho_Raw_Data_DTO_Version_2 entity);

        // xóa sản phẩm khỏi phiếu và xóa trong DM_Nhap_Kho_Raw_Data
        Task<Response> DeleteAsync(DM_Xuat_Kho_Raw_Data entity);

        Task<Response> UpdateAsync(DM_Xuat_Kho_Raw_Data_DTO_Edit entity);
    }
}
