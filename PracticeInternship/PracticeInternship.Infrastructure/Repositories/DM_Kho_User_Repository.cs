using Microsoft.EntityFrameworkCore;
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
    public class DM_Kho_User_Repository(PracticeInternshipDbContext context) : Interface_DM_Kho_User
    {
        public async Task<Response> CreateAsync(DM_Kho_User entity)
        {
            try
            {
                // check null
                if (string.IsNullOrEmpty(entity.Ma_Dang_Nhap))
                {
                    return new Response(false, $"Ma_Dang_Nhap of warehouse is not Empty!!");
                }
                if (entity.Kho_Id == 0)
                {
                    return new Response(false, $"Kho_Id of warehouse is not null!!");
                }
                var getKhoUserExists = await GetByAsync(_ => _.Ma_Dang_Nhap!.Equals(entity.Ma_Dang_Nhap) &&
                                                    _.Kho_Id.Equals(entity.Kho_Id));

                if (getKhoUserExists != null)
                {
                    throw new Exception("MaDangNhap and KhoId is already!");
                }

                var currentEntity = context.DM_Kho_User.Add(entity).Entity;
                await context.SaveChangesAsync();

                if (currentEntity != null && currentEntity.Id > 0)
                {
                    return new Response(true, $"{entity.Ma_Dang_Nhap} add authenticated to database successfully");
                }
                else
                {
                    return new Response(false, $"Error occurred while adding authenticate {entity.Ma_Dang_Nhap}");
                }
            }
            catch (Exception ex)
            {
                return new Response(false, $"Error occurred adding new authentication for warehouse, ${ex.Message}");
            }
        }

        public async Task<Response> DeleteAsync(DM_Kho_User entity)
        {
            try
            {
                var khoUser = await GetByIdAsync(entity.Id);
                if (khoUser == null)
                {
                    return new Response(false, $"{entity.Ma_Dang_Nhap} not found");
                }

                context.Entry(khoUser).State = EntityState.Deleted;
                context.DM_Kho_User.Remove(entity);
                await context.SaveChangesAsync();
                return new Response(true, $"{khoUser.Ma_Dang_Nhap} is deleted successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred deleting DM_Kho_User, {ex.Message}");
            }
        }

        public async Task<IEnumerable<DM_Kho_User>> GetAllAsync()
        {
            try
            {
                var list = await context.DM_Kho_User.AsNoTracking().ToListAsync();
                return list is not null ? list : null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred retrieving DM_Kho_User, {ex.Message}");
            }
        }

        public async Task<DM_Kho_User> GetByAsync(Expression<Func<DM_Kho_User, bool>> predicate)
        {
            try
            {
                var khoUser = await context.DM_Kho_User.Where(predicate).FirstOrDefaultAsync();
                return khoUser is not null ? khoUser : null!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DM_Kho_User> GetByIdAsync(int id)
        {
            try
            {
                var khoUser = await context.DM_Kho_User.FindAsync(id);
                return khoUser is not null ? khoUser : null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred retrieving DM_Kho_User, {ex.Message}");
            }
        }

        public Task<Response> UpdateAsync(DM_Kho_User entity)
        {
            throw new NotImplementedException();
        }
    }
}
