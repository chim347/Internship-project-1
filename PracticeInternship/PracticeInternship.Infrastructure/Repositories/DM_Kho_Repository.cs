using Microsoft.EntityFrameworkCore;
using PracticeInternship.Application.Interfaces;
using PracticeInternship.Application.Responses;
using PracticeInternship.Domain.Entities;
using PracticeInternship.Infrastructure.Data;
using System.Linq.Expressions;

namespace PracticeInternship.Infrastructure.Repositories
{
    public class DM_Kho_Repository(PracticeInternshipDbContext context) : Interface_DM_Kho
    {
        public async Task<Response> CreateAsync(DM_Kho entity)
        {
            try
            {
                // check null
                if (string.IsNullOrEmpty(entity.Ten_Kho))
                {
                    return new Response(false, $"Name of warehouse is not Empty!!");
                }

                // check Ten_NCC va Ma_NCC is already exist
                var getTenKho = await GetByAsync(_ => _.Ten_Kho!.Equals(entity.Ten_Kho));
                if (getTenKho != null && !string.IsNullOrEmpty(getTenKho.Ten_Kho))
                {
                    return new Response(false, $"{entity.Ten_Kho} already added");
                }


                var currentEntity = context.DM_Kho.Add(entity).Entity;
                await context.SaveChangesAsync();

                if (currentEntity != null && currentEntity.Id > 0)
                {
                    return new Response(true, $"{entity.Ten_Kho} added to database successfully");
                }
                else
                {
                    return new Response(false, $"Error occurred while adding {entity.Ten_Kho}");
                }
            }
            catch (Exception ex)
            {
                return new Response(false, $"Error occurred adding new DM_Kho, ${ex.Message}");
            }
        }

        public async Task<Response> DeleteAsync(DM_Kho entity)
        {
            try
            {
                var kho = await GetByIdAsync(entity.Id);
                if (kho == null)
                {
                    return new Response(false, $"{entity.Ten_Kho} not found");
                }

                context.Entry(kho).State = EntityState.Deleted;
                context.DM_Kho.Remove(entity);
                await context.SaveChangesAsync();
                return new Response(true, $"{kho.Ten_Kho} is deleted successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred deleting DM_Kho, {ex.Message}");
            }
        }

        public async Task<IEnumerable<DM_Kho>> GetAllAsync()
        {
            try
            {
                var list = await context.DM_Kho.AsNoTracking().ToListAsync();
                return list is not null ? list : null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred retrieving DM_Kho, {ex.Message}");
            }
        }

        public async Task<DM_Kho> GetByAsync(Expression<Func<DM_Kho, bool>> predicate)
        {
            try
            {
                var kho = await context.DM_Kho.Where(predicate).FirstOrDefaultAsync();
                return kho is not null ? kho : null!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DM_Kho> GetByIdAsync(int id)
        {
            try
            {
                var kho = await context.DM_Kho.FindAsync(id);
                return kho is not null ? kho : null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred retrieving DM_Kho, {ex.Message}");
            }
        }

        public async Task<Response> UpdateAsync(DM_Kho entity)
        {
            try
            {
                var kho = await GetByIdAsync(entity.Id);
                if (kho == null)
                {
                    return new Response(false, $"{entity.Ten_Kho} not found");
                }

                // check null
                if (string.IsNullOrEmpty(entity.Ten_Kho))
                {
                    return new Response(false, $"Name of warehouse is not Empty!!");
                }

                // check Ten_Loai_San_Pham va Ma_LSP is already exist
                if (!string.Equals(kho.Ten_Kho, entity.Ten_Kho, StringComparison.OrdinalIgnoreCase))
                {
                    var getTenKho = await GetByAsync(_ => _.Ten_Kho!.Equals(entity.Ten_Kho));
                    if (getTenKho != null && !string.IsNullOrEmpty(getTenKho.Ten_Kho))
                    {
                        return new Response(false, $"{entity.Ten_Kho} already added");
                    }
                }

                context.Entry(kho).State = EntityState.Detached;
                context.DM_Kho.Update(entity);
                await context.SaveChangesAsync();

                return new Response(true, $"{entity.Ten_Kho} is update successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred updating existing DM_Kho, {ex.Message}");
            }
        }
    }
}
