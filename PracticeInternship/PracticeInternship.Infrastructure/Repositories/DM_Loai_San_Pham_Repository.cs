using Microsoft.EntityFrameworkCore;
using PracticeInternship.Application.Interfaces;
using PracticeInternship.Application.Responses;
using PracticeInternship.Domain.Entities;
using PracticeInternship.Infrastructure.Data;
using System.Linq;
using System.Linq.Expressions;

namespace PracticeInternship.Infrastructure.Repositories
{
    public class DM_Loai_San_Pham_Repository(PracticeInternshipDbContext context) : Interface_DM_Loai_San_Pham
    {
        public async Task<Response> CreateAsync(DM_Loai_San_Pham entity)
        {
            try
            {
                // check null
                if (string.IsNullOrEmpty(entity.Ten_LSP) || string.IsNullOrEmpty(entity.Ma_LSP))
                {
                    return new Response(false, $"Name or Ma_LSP of product is not Empty!!");
                }

                // check Ten_Loai_San_Pham va Ma_LSP is already exist
                var getTenLoaiSanPham = await GetByAsync(_ => _.Ten_LSP!.Equals(entity.Ten_LSP));
                if (getTenLoaiSanPham != null && !string.IsNullOrEmpty(getTenLoaiSanPham.Ten_LSP))
                {
                    return new Response(false, $"{entity.Ten_LSP} already added");
                }
                var getMaLSP = await GetByAsync(_ => _.Ma_LSP!.Equals(entity.Ma_LSP));
                if (getMaLSP != null && !string.IsNullOrEmpty(getMaLSP.Ma_LSP))
                {
                    return new Response(false, $"{entity.Ma_LSP} already added");
                }

                var currentEntity = context.DM_Loai_San_Pham.Add(entity).Entity;
                await context.SaveChangesAsync();

                if (currentEntity != null && currentEntity.Id > 0)
                {
                    return new Response(true, $"{entity.Ten_LSP} added to database successfully");
                }
                else
                {
                    return new Response(false, $"Error occurred while adding {entity.Ten_LSP}");
                }
            }
            catch (Exception ex)
            {
                return new Response(false, $"Error occurred adding new DM_Loai_San_Pham, ${ex.Message}");
            }
        }

        public async Task<Response> DeleteAsync(DM_Loai_San_Pham entity)
        {
            try
            {
                var loaiSanPham = await GetByIdAsync(entity.Id);
                if (loaiSanPham == null)
                {
                    return new Response(false, $"{entity.Ten_LSP} not found");
                }

                context.Entry(loaiSanPham).State = EntityState.Deleted;
                context.DM_Loai_San_Pham.Remove(entity);
                await context.SaveChangesAsync();
                return new Response(true, $"{loaiSanPham.Ten_LSP} is deleted successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred deleting DM_Loai_San_Pham, {ex.Message}");
            }
        }

        public async Task<IEnumerable<DM_Loai_San_Pham>> GetAllAsync()
        {
            try
            {
                var list = await context.DM_Loai_San_Pham.AsNoTracking().ToListAsync();
                return list is not null ? list : null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred retrieving DM_Loai_San_Pham, {ex.Message}");
            }
        }

        public async Task<DM_Loai_San_Pham> GetByAsync(Expression<Func<DM_Loai_San_Pham, bool>> predicate)
        {
            try
            {
                var loaiSanPham = await context.DM_Loai_San_Pham.Where(predicate).FirstOrDefaultAsync();
                return loaiSanPham is not null ? loaiSanPham : null!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DM_Loai_San_Pham> GetByIdAsync(int id)
        {
            try
            {
                var loaiSanPham = await context.DM_Loai_San_Pham.FindAsync(id);
                return loaiSanPham is not null ? loaiSanPham : null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred retrieving DM_Loai_San_Pham, {ex.Message}");
            }
        }

        public async Task<Response> UpdateAsync(DM_Loai_San_Pham entity)
        {
            try
            {
                var loaiSanPham = await GetByIdAsync(entity.Id);
                if (loaiSanPham == null)
                {
                    return new Response(false, $"{entity.Ten_LSP} not found");
                }

                // check null
                if (string.IsNullOrEmpty(entity.Ten_LSP) || string.IsNullOrEmpty(entity.Ma_LSP))
                {
                    return new Response(false, $"Name or Ma_LSP of product is not Empty!!");
                }

                // check Ten_Loai_San_Pham va Ma_LSP is already exist
                if (!string.Equals(loaiSanPham.Ten_LSP, entity.Ten_LSP, StringComparison.OrdinalIgnoreCase) ||
                    !string.Equals(loaiSanPham.Ma_LSP, entity.Ma_LSP, StringComparison.OrdinalIgnoreCase))
                {
                    var getTenLSP = await GetByAsync(_ => _.Ten_LSP!.Equals(entity.Ten_LSP));
                    if (getTenLSP != null && !string.IsNullOrEmpty(getTenLSP.Ten_LSP))
                    {
                        return new Response(false, $"{entity.Ten_LSP} already added");
                    }
                    var getMaLSP = await GetByAsync(_ => _.Ma_LSP!.Equals(entity.Ma_LSP));
                    if (getMaLSP != null && !string.IsNullOrEmpty(getMaLSP.Ma_LSP))
                    {
                        return new Response(false, $"{entity.Ma_LSP} already added");
                    }
                }

                context.Entry(loaiSanPham).State = EntityState.Detached;
                context.DM_Loai_San_Pham.Update(entity);
                await context.SaveChangesAsync();

                return new Response(true, $"{entity.Ten_LSP} is update successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred updating existing DM_Loai_San_Pham, {ex.Message}");
            }
        }
    }
}
