using Microsoft.EntityFrameworkCore;
using PracticeInternship.Application.DTOs;
using PracticeInternship.Application.Interfaces;
using PracticeInternship.Application.Responses;
using PracticeInternship.Domain.Entities;
using PracticeInternship.Infrastructure.Data;

namespace PracticeInternship.Infrastructure.Repositories
{
    public class DM_Nhap_Kho_Repository : Interface_DM_Nhap_Kho
    {
        private readonly PracticeInternshipDbContext context;

        public DM_Nhap_Kho_Repository(PracticeInternshipDbContext _context)
        {
            context = _context;
        }

        public async Task<Response> CreateAsync(DM_Nhap_Kho_DTO entity)
        {
            try
            {
                // check null
                if (string.IsNullOrEmpty(entity.So_Phieu_Nhap_Kho))
                    return new Response(false, "Số phiếu nhập không được rỗng.");
                if (await context.DM_Nhap_Kho.AnyAsync(x => x.So_Phieu_Nhap_Kho == entity.So_Phieu_Nhap_Kho))
                    return new Response(false, "Số phiếu nhập đã tồn tại.");
                if (string.IsNullOrEmpty(entity.Kho_Id))
                    return new Response(false, "Kho không được rỗng.");
                if (string.IsNullOrEmpty(entity.NCC_Id))
                    return new Response(false, "Nhà cung cấp không được rỗng.");
                if (entity.Ngay_Nhap_kho == default)
                    return new Response(false, "Ngày nhập kho không được rỗng.");

                var dmNhapKho = new DM_Nhap_Kho
                {
                    So_Phieu_Nhap_Kho = entity.So_Phieu_Nhap_Kho,
                    Kho_Id = Guid.Parse(entity.Kho_Id),
                    NCC_Id = Guid.Parse(entity.NCC_Id),
                    Ngay_Nhap_kho = entity.Ngay_Nhap_kho,
                    Ghi_Chu = entity.Ghi_Chu,
                };
                context.DM_Nhap_Kho.Add(dmNhapKho);

                var list = new List<DM_Nhap_Kho_Raw_Data>();
                if (entity.List_DM_Nhap_Kho_Raw_Data_DTO != null &&
                    entity.List_DM_Nhap_Kho_Raw_Data_DTO.Any())
                {
                    foreach (var item in entity.List_DM_Nhap_Kho_Raw_Data_DTO)
                    {
                        var dmNhapKhoRawData = new DM_Nhap_Kho_Raw_Data
                        {
                            Nhap_Kho_Id = dmNhapKho.Id,
                            San_Pham_Id = Guid.Parse(item.San_Pham_Id!),
                            SL_Nhap = item.SL_Nhap,
                            Don_Gia_Nhap = item.Don_Gia_Nhap,
                        };
                        list.Add(dmNhapKhoRawData);
                    }
                    context.DM_Nhap_Kho_Raw_Data.AddRange(list);
                }
                await context.SaveChangesAsync();

                return new Response(true, $"{entity.So_Phieu_Nhap_Kho} added to database successfully");

            }
            catch (Exception ex)
            {
                return new Response(false, $"Error occurred adding new DM_Nhap_Kho, ${ex.Message}");
            }
        }

        public async Task<Response> DeleteAsync(DM_Nhap_Kho entity)
        {
            try
            {
                var dmNhapKho = await context.DM_Nhap_Kho.Where(nk => nk.Id == entity.Id).SingleOrDefaultAsync();
                if (dmNhapKho == null)
                {
                    return new Response(false, $"{entity.So_Phieu_Nhap_Kho} not found");
                }
                var dmNhapKhoRawData = await context.DM_Nhap_Kho_Raw_Data.Where(nkr => nkr.Nhap_Kho_Id == dmNhapKho.Id).ToListAsync();
                if (dmNhapKhoRawData != null && dmNhapKhoRawData.Any())
                {
                    context.DM_Nhap_Kho_Raw_Data.RemoveRange(dmNhapKhoRawData);
                }

                context.DM_Nhap_Kho.Remove(dmNhapKho);
                await context.SaveChangesAsync();
                return new Response(true, $"{dmNhapKho.So_Phieu_Nhap_Kho} is deleted successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred deleting DM_Nhap_Kho, {ex.Message}");
            }
        }

        public async Task<IEnumerable<DM_Nhap_Kho>> GetAllAsync()
        {
            try
            {
                var list = await context.DM_Nhap_Kho.AsNoTracking().ToListAsync();
                return list is not null ? list : null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred retrieving DM_Nhap_Kho, {ex.Message}");
            }
        }

        public async Task<DM_Nhap_Kho> GetByIdAsync(Guid id)
        {
            try
            {
                var dmNhapKho = await context.DM_Nhap_Kho.Where(nk => nk.Id == id).SingleOrDefaultAsync();
                return dmNhapKho is not null ? dmNhapKho : null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred retrieving DM_Nhap_Kho, {ex.Message}");
            }
        }
    }
}
