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
    }
}
