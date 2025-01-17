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
    }
}
