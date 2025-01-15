using Microsoft.EntityFrameworkCore;
using PracticeInternship.Application.Interfaces;
using PracticeInternship.Application.Responses;
using PracticeInternship.Domain.Entities;
using PracticeInternship.Infrastructure.Data;
using System.Linq.Expressions;

namespace PracticeInternship.Infrastructure.Repositories
{
    public class DM_NCC_Repository(PracticeInternshipDbContext context) : Interface_DM_NCC
    {
        public async Task<Response> CreateAsync(DM_NCC entity)
        {
            try
            {
                // check null
                if (string.IsNullOrEmpty(entity.Ma_NCC) || string.IsNullOrEmpty(entity.Ten_NCC))
                {
                    return new Response(false, $"Name or Code of NCC is not Empty!!");
                }

                // check Ten_NCC va Ma_NCC is already exist
                var getTenNCC = await GetByAsync(_ => _.Ten_NCC!.Equals(entity.Ten_NCC));
                if (getTenNCC != null && !string.IsNullOrEmpty(getTenNCC.Ten_NCC))
                {
                    return new Response(false, $"{entity.Ten_NCC} already added");
                }
                var getMaNCC = await GetByAsync(_ => _.Ma_NCC!.Equals(entity.Ma_NCC));
                if (getMaNCC != null && !string.IsNullOrEmpty(getMaNCC.Ma_NCC))
                {
                    return new Response(false, $"{entity.Ma_NCC} already added");
                }

                var currentEntity = context.DM_NCC.Add(entity).Entity;
                await context.SaveChangesAsync();

                if (currentEntity != null)
                {
                    return new Response(true, $"{entity.Ten_NCC} added to database successfully");
                }
                else
                {
                    return new Response(false, $"Error occurred while adding {entity.Ten_NCC}");
                }
            }
            catch (Exception ex)
            {
                return new Response(false, $"Error occurred adding new DM_NCC, ${ex.Message}");
            }
        }

        public async Task<Response> DeleteAsync(DM_NCC entity)
        {
            try
            {
                var ncc = await GetByIdAsync(entity.Id);
                if (ncc == null)
                {
                    return new Response(false, $"{entity.Ten_NCC} not found");
                }

                context.Entry(ncc).State = EntityState.Deleted;
                context.DM_NCC.Remove(entity);
                await context.SaveChangesAsync();
                return new Response(true, $"{ncc.Ten_NCC} is deleted successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred deleting DM_NCC, {ex.Message}");
            }
        }

        public async Task<IEnumerable<DM_NCC>> GetAllAsync()
        {
            try
            {
                var list = await context.DM_NCC.AsNoTracking().ToListAsync();
                return list is not null ? list : null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred retrieving DM_NCC, {ex.Message}");
            }
        }

        public async Task<DM_NCC> GetByAsync(Expression<Func<DM_NCC, bool>> predicate)
        {
            try
            {
                var ncc = await context.DM_NCC.Where(predicate).FirstOrDefaultAsync();
                return ncc is not null ? ncc : null!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DM_NCC> GetByIdAsync(Guid id)
        {
            try
            {
                var ncc = await context.DM_NCC.Where(ncc => ncc.Id == id).SingleOrDefaultAsync();
                return ncc is not null ? ncc : null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred retrieving DM_NCC, {ex.Message}");
            }
        }

        public async Task<Response> UpdateAsync(DM_NCC entity)
        {
            try
            {
                var ncc = await context.DM_NCC.Where(ncc => ncc.Id == entity.Id).AsNoTracking().SingleOrDefaultAsync();
                if (ncc == null)
                {
                    return new Response(false, $"{entity.Ten_NCC} not found");
                }
                
                context.Entry(ncc).State = EntityState.Detached;

                // check null
                if (string.IsNullOrEmpty(entity.Ten_NCC) || string.IsNullOrEmpty(entity.Ma_NCC))
                {
                    return new Response(false, $"Name or Code of NCC is not Empty!!");
                }

                // check Ten_Loai_San_Pham va Ma_LSP is already exist
                if (!string.Equals(ncc.Ma_NCC, entity.Ma_NCC, StringComparison.OrdinalIgnoreCase))
                {
                    var getMaNCC = await GetByAsync(_ => _.Ma_NCC!.Equals(entity.Ma_NCC));
                    if (getMaNCC != null && !string.IsNullOrEmpty(getMaNCC.Ma_NCC))
                    {
                        return new Response(false, $"{entity.Ma_NCC} already added");
                    }
                }
                if(!string.Equals(ncc.Ten_NCC, entity.Ten_NCC, StringComparison.OrdinalIgnoreCase))
                {
                    var getTenNCC = await GetByAsync(_ => _.Ten_NCC!.Equals(entity.Ten_NCC));
                    if (getTenNCC != null && !string.IsNullOrEmpty(getTenNCC.Ten_NCC))
                    {
                        return new Response(false, $"{entity.Ten_NCC} already added");
                    }
                }

                context.DM_NCC.Update(entity);
                await context.SaveChangesAsync();

                return new Response(true, $"{entity.Ten_NCC} is update successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred updating existing DM_NCC, {ex.Message}");
            }
        }
    }
}
