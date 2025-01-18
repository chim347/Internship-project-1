using PracticeInternship.Application.DTOs;
using PracticeInternship.Application.Responses;
using PracticeInternship.Domain.Entities;

namespace PracticeInternship.Application.Interfaces
{
    public interface Interface_DM_Xuat_kho
    {
        Task<Response> CreateAsync(DM_Xuat_Kho_DTO entity);
        Task<Response> DeleteAsync(DM_Xuat_Kho entity);
        Task<IEnumerable<DM_Xuat_Kho>> GetAllAsync();
        Task<DM_Xuat_Kho> GetByIdAsync(Guid id);

        // hiệu chỉnh phiếu xuất cho phần header
        Task<Response> UpdateAsync(DM_Xuat_Kho entity);

        // get detail của 1 phiếu xuất kho với các thông tin từ DM_Xuat_Kho_Raw_Data
        Task<DM_Xuat_Kho_Detail_Response> GetDetailOfXuatKhoById(Guid id);

        // get danh sách detail của 1 phiếu xuất kho với các
        // thông tin từ DM_Xuat_Kho_Raw_Data bằng nút search ngày xuất
        Task<IEnumerable<DM_Xuat_Kho_Search_Ngay_Nhap_kho_Response>> GetAllDetailOfXuatKhoBySearchDate(string startDate, string endDate);
    }
}
