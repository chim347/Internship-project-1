using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PracticeInternship.Application.Interfaces;
using PracticeInternship.Application.Responses;
using PracticeInternship.Domain.Entities;
using PracticeInternship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInternship.Infrastructure.Repositories
{
    public class DM_San_Pham_Repository(PracticeInternshipDbContext context) : Interface_DM_San_Pham
    {
        public async Task<Response> CreateAsync(DM_San_Pham entity)
        {
            try
            {
                // check null
                if (entity.Don_Vi_Tinh_Id == 0 || entity.Loai_San_Pham_Id == 0)
                {
                    return new Response(false, $"Don_Vi_Tinh_Id or Loai_San_Pham_Id of product is not null!!");
                }
                if (string.IsNullOrEmpty(entity.Ten_San_Pham) || string.IsNullOrEmpty(entity.Ma_San_Pham))
                {
                    return new Response(false, $"Name or Code of product is not Empty!!");
                }

                // check Ma_SP is already exist
                var getMaSP = await GetByAsync(_ => _.Ma_San_Pham!.Equals(entity.Ma_San_Pham));
                if (getMaSP != null && !string.IsNullOrEmpty(getMaSP.Ma_San_Pham))
                {
                    return new Response(false, $"{entity.Ma_San_Pham} already added");
                }

                var currentEntity = context.DM_San_Pham.Add(entity).Entity;
                await context.SaveChangesAsync();

                if (currentEntity != null && currentEntity.Id > 0)
                {
                    return new Response(true, $"{entity.Ten_San_Pham} added to database successfully");
                }
                else
                {
                    return new Response(false, $"Error occurred while adding {entity.Ten_San_Pham}");
                }
            }
            catch (Exception ex)
            {
                return new Response(false, $"Error occurred adding new DM_San_Pham, ${ex.Message}");
            }
        }

        public async Task<Response> DeleteAsync(DM_San_Pham entity)
        {
            try
            {
                var sanPham = await GetByIdAsync(entity.Id);
                if (sanPham == null)
                {
                    return new Response(false, $"{entity.Ten_San_Pham} not found");
                }

                context.Entry(sanPham).State = EntityState.Deleted;
                context.DM_San_Pham.Remove(entity);
                await context.SaveChangesAsync();
                return new Response(true, $"{sanPham.Ten_San_Pham} is deleted successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred deleting DM_San_Pham, {ex.Message}");
            }
        }

        public async Task<IEnumerable<DM_San_Pham>> GetAllAsync()
        {
            try
            {
                var list = await context.DM_San_Pham.AsNoTracking().ToListAsync();
                return list is not null ? list : null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred retrieving DM_San_Pham, {ex.Message}");
            }
        }

        public async Task<DM_San_Pham> GetByAsync(Expression<Func<DM_San_Pham, bool>> predicate)
        {
            try
            {
                var sanPham = await context.DM_San_Pham.Where(predicate).FirstOrDefaultAsync();
                return sanPham is not null ? sanPham : null!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DM_San_Pham> GetByIdAsync(int id)
        {
            try
            {
                var sanPham = await context.DM_San_Pham.FindAsync(id);
                return sanPham is not null ? sanPham : null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred retrieving DM_San_Pham, {ex.Message}");
            }
        }

        public async Task<Response> UpdateAsync(DM_San_Pham entity)
        {
            try
            {
                var sanPham = await GetByIdAsync(entity.Id);
                if (sanPham == null)
                {
                    return new Response(false, $"{entity.Ten_San_Pham} not found");
                }

                // check null
                if (entity.Don_Vi_Tinh_Id == 0 || entity.Loai_San_Pham_Id == 0)
                {
                    return new Response(false, $"Don_Vi_Tinh_Id or Loai_San_Pham_Id of product is not null!!");
                }
                if (string.IsNullOrEmpty(entity.Ten_San_Pham) || string.IsNullOrEmpty(entity.Ma_San_Pham))
                {
                    return new Response(false, $"Name or Code of product is not Empty!!");
                }

                // check Ma_SP is already exist
                if (!string.Equals(sanPham.Ma_San_Pham, entity.Ma_San_Pham, StringComparison.OrdinalIgnoreCase))
                {
                    var getMaSP = await GetByAsync(_ => _.Ma_San_Pham!.Equals(entity.Ma_San_Pham));
                    if (getMaSP != null && !string.IsNullOrEmpty(getMaSP.Ma_San_Pham))
                    {
                        return new Response(false, $"{entity.Ma_San_Pham} already added");
                    }
                }

                context.Entry(sanPham).State = EntityState.Detached;
                context.DM_San_Pham.Update(entity);
                await context.SaveChangesAsync();

                return new Response(true, $"{entity.Ten_San_Pham} is update successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred updating existing DM_San_Pham, {ex.Message}");
            }
        }
    }
}
