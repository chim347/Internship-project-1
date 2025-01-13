using Microsoft.EntityFrameworkCore;
using PracticeInternship.Application.Interfaces;
using PracticeInternship.Application.Responses;
using PracticeInternship.Domain.Entities;
using PracticeInternship.Infrastructure.Data;
using System.Linq.Expressions;

namespace PracticeInternship.Infrastructure.Repositories
{
    public class DM_Don_Vi_Tinh_Repository(PracticeInternshipDbContext context) : Interface_DM_Don_Vi_Tinh
    {
        public async Task<Response> CreateAsync(DM_Don_Vi_Tinh entity)
        {
            try
            {
                // check null
                if(string.IsNullOrEmpty(entity.Ten_Don_Vi_Tinh))
                {
                    return new Response(false, $"Tên đơn vị không được rỗng!!");
                }

                // check Ten_Don_Vi_Tinh is already exist
                var getDonViTinh = await GetByAsync(_ => _.Ten_Don_Vi_Tinh!.Equals(entity.Ten_Don_Vi_Tinh));
                if (getDonViTinh != null && !string.IsNullOrEmpty(getDonViTinh.Ten_Don_Vi_Tinh))
                {
                    return new Response(false, $"{entity.Ten_Don_Vi_Tinh} already added");
                }
                var currentEntity = context.DM_Don_Vi_Tinh.Add(entity).Entity;
                await context.SaveChangesAsync();

                if (currentEntity != null && currentEntity.Id > 0)
                {
                    return new Response(true, $"{entity.Ten_Don_Vi_Tinh} added to database successfully");
                }
                else
                {
                    return new Response(false, $"Error occurred while adding {entity.Ten_Don_Vi_Tinh}");
                }
            }
            catch (Exception ex)
            {
                return new Response(false, $"Error occurred adding new DM_Don_Vi_Tinh, ${ex.Message}");
            }
        }

        public async Task<Response> DeleteAsync(DM_Don_Vi_Tinh entity)
        {
            try
            {
                var donViTinh = await GetByIdAsync(entity.Id);
                if (donViTinh == null)
                {
                    return new Response(false, $"{entity.Ten_Don_Vi_Tinh} not found");
                }

                context.Entry(donViTinh).State = EntityState.Deleted;
                context.DM_Don_Vi_Tinh.Remove(entity);
                await context.SaveChangesAsync();
                return new Response(true, $"{donViTinh.Ten_Don_Vi_Tinh} is deleted successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred deleting DM_Don_Vi_Tinh, {ex.Message}");
            }
        }

        public async Task<IEnumerable<DM_Don_Vi_Tinh>> GetAllAsync()
        {
            try
            {
                var list = await context.DM_Don_Vi_Tinh.AsNoTracking().ToListAsync();
                return list is not null ? list : null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred retrieving DM_Don_Vi_Tinhs, {ex.Message}");
            }
        }

        public async Task<DM_Don_Vi_Tinh> GetByAsync(Expression<Func<DM_Don_Vi_Tinh, bool>> predicate)
        {
            try
            {
                var donViTinh = await context.DM_Don_Vi_Tinh.Where(predicate).FirstOrDefaultAsync();
                return donViTinh is not null ? donViTinh : null!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DM_Don_Vi_Tinh> GetByIdAsync(int id)
        {
            try
            {
                var donViTinh = await context.DM_Don_Vi_Tinh.FindAsync(id);
                return donViTinh is not null ? donViTinh : null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred retrieving DM_Don_Vi_Tinh, {ex.Message}");
            }
        }

        public async Task<Response> UpdateAsync(DM_Don_Vi_Tinh entity)
        {
            try
            {
                var donViTinh = await GetByIdAsync(entity.Id);  
                if(donViTinh == null)
                {
                    return new Response(false, $"{entity.Ten_Don_Vi_Tinh} not found");
                }

                // check Ten_Don_Vi_Tinh is already exist
                var getDonViTinh = await GetByAsync(_ => _.Ten_Don_Vi_Tinh!.Equals(entity.Ten_Don_Vi_Tinh));
                if (getDonViTinh != null && !string.IsNullOrEmpty(getDonViTinh.Ten_Don_Vi_Tinh))
                {
                    return new Response(false, $"{entity.Ten_Don_Vi_Tinh} already added");
                }

                context.Entry(donViTinh).State = EntityState.Detached;
                context.DM_Don_Vi_Tinh.Update(entity);
                await context.SaveChangesAsync();   

                return new Response(true, $"{entity.Ten_Don_Vi_Tinh} is update successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred updating existing DM_Don_Vi_Tinh, {ex.Message}");
            }
        }
    }
}
