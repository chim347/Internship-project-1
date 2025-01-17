using Microsoft.EntityFrameworkCore;
using PracticeInternship.Application.Interfaces;
using PracticeInternship.Domain.Entities;
using PracticeInternship.Infrastructure.Data;

namespace PracticeInternship.Infrastructure.Repositories
{
    public class XNK_Xuat_Kho_Repository : Interface_XNK_Xuat_Kho
    {
        private readonly PracticeInternshipDbContext context;

        public XNK_Xuat_Kho_Repository(PracticeInternshipDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<XNK_Xuat_Kho>> GetAllAsync()
        {
            try
            {
                var list = await context.XNK_Xuat_Kho.AsNoTracking().ToListAsync();
                return list is not null ? list : null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred retrieving XNK_Xuat_Kho, {ex.Message}");
            }
        }

        public async Task<bool> GetByIdAsync(string id)
        {
            try
            {
                return await context.XNK_Xuat_Kho.AnyAsync(nk => nk.Xuat_Kho_Id == Guid.Parse(id));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred retrieving XNK_Xuat_Kho, {ex.Message}");
            }
        }
    }
}
