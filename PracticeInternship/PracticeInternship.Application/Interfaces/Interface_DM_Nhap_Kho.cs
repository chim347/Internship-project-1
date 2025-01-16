using PracticeInternship.Application.DTOs;
using PracticeInternship.Application.Responses;
using PracticeInternship.Domain.Entities;

namespace PracticeInternship.Application.Interfaces
{
    public interface Interface_DM_Nhap_Kho
    {
        Task<Response> CreateAsync(DM_Nhap_Kho_DTO entity);
        Task<Response> DeleteAsync(DM_Nhap_Kho entity);
        Task<IEnumerable<DM_Nhap_Kho>> GetAllAsync();
        Task<DM_Nhap_Kho> GetByIdAsync(Guid id);

        // hiệu chỉnh phiếu nhập cho phần header
        Task<Response> UpdateAsync(DM_Nhap_Kho entity);

        // get detail của 1 phiếu nhập kho với các thông tin từ DM_Nhap_Kho_Raw_Data
        Task<DM_Nhap_Kho_Detail_Response> GetDetailOfNhapKhoById(Guid id);

    }
}
