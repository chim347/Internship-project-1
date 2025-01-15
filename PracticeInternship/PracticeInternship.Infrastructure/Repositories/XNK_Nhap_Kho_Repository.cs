using Microsoft.EntityFrameworkCore;
using PracticeInternship.Application.Interfaces;
using PracticeInternship.Domain.Entities;
using PracticeInternship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInternship.Infrastructure.Repositories
{
    public class XNK_Nhap_Kho_Repository : Interface_XNK_Nhap_Kho
    {
        private readonly PracticeInternshipDbContext context;

        public XNK_Nhap_Kho_Repository(PracticeInternshipDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<XNK_Nhap_Kho>> GetAllAsync()
        {
            try
            {
                var list = await context.XNK_Nhap_Kho.AsNoTracking().ToListAsync();
                return list is not null ? list : null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred retrieving DM_San_Pham, {ex.Message}");
            }
        }

        public async Task<bool> GetByIdAsync(string id)
        {
            try
            {
                return await context.XNK_Nhap_Kho.AnyAsync(nk => nk.Nhap_Kho_Id == Guid.Parse(id));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred retrieving DM_Nhap_Kho, {ex.Message}");
            }
        }
    }
}
