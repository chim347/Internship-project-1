using Microsoft.EntityFrameworkCore;
using PracticeInternship.Application.DTOs;
using PracticeInternship.Application.Interfaces;
using PracticeInternship.Application.Responses;
using PracticeInternship.Domain.Entities;
using PracticeInternship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInternship.Infrastructure.Repositories
{
    public class DM_Xuat_Kho_Repository : Interface_DM_Xuat_kho
    {
        private readonly PracticeInternshipDbContext context;

        public DM_Xuat_Kho_Repository(PracticeInternshipDbContext _context)
        {
            context = _context;
        }

        public async Task<Response> CreateAsync(DM_Xuat_Kho_DTO entity)
        {
            try
            {
                // check null
                if (string.IsNullOrEmpty(entity.So_Phieu_Xuat_Kho))
                    return new Response(false, "Số phiếu xuất không được rỗng.");
                if (await context.DM_Xuat_Kho.AnyAsync(x => x.So_Phieu_Xuat_Kho == entity.So_Phieu_Xuat_Kho))
                    return new Response(false, "Số phiếu xuất đã tồn tại.");
                if (string.IsNullOrEmpty(entity.Kho_Id))
                    return new Response(false, "Kho không được rỗng.");
                if (entity.Ngay_Xuat_Kho == default)
                    return new Response(false, "Ngày nhập kho không được rỗng.");

                var dmXuatKho = new DM_Xuat_Kho
                {
                    So_Phieu_Xuat_Kho = entity.So_Phieu_Xuat_Kho,
                    Kho_Id = Guid.Parse(entity.Kho_Id),
                    Ngay_Xuat_Kho = entity.Ngay_Xuat_Kho,
                    Ghi_Chu = entity.Ghi_Chu,
                };
                context.DM_Xuat_Kho.Add(dmXuatKho);

                var list = new List<DM_Xuat_Kho_Raw_Data>();
                if (entity.List_DM_Xuat_Kho_Raw_Data_DTO != null &&
                    entity.List_DM_Xuat_Kho_Raw_Data_DTO.Any())
                {
                    foreach (var item in entity.List_DM_Xuat_Kho_Raw_Data_DTO)
                    {
                        var dmXuatKhoRawData = new DM_Xuat_Kho_Raw_Data
                        {
                            Xuat_Kho_Id = dmXuatKho.Id,
                            San_Pham_Id = Guid.Parse(item.San_Pham_Id!),
                            SL_Xuat = item.SL_Xuat,
                            Don_Gia_Xuat = item.Don_Gia_Xuat,
                        };
                        list.Add(dmXuatKhoRawData);
                    }
                    context.DM_Xuat_Kho_Raw_Data.AddRange(list);
                }
                await context.SaveChangesAsync();

                return new Response(true, $"{entity.So_Phieu_Xuat_Kho} added to database successfully");

            }
            catch (Exception ex)
            {
                return new Response(false, $"Error occurred adding new DM_Nhap_Kho, ${ex.Message}");
            }
        }

        public async Task<Response> DeleteAsync(DM_Xuat_Kho entity)
        {
            try
            {
                var dmXuatKho = await context.DM_Xuat_Kho.Where(nk => nk.Id == entity.Id).SingleOrDefaultAsync();
                if (dmXuatKho == null)
                {
                    return new Response(false, $"{entity.So_Phieu_Xuat_Kho} not found");
                }
                var dmXuatKhoRawData = await context.DM_Xuat_Kho_Raw_Data.Where(nkr => nkr.Xuat_Kho_Id == dmXuatKho.Id).ToListAsync();
                if (dmXuatKhoRawData != null && dmXuatKhoRawData.Any())
                {
                    context.DM_Xuat_Kho_Raw_Data.RemoveRange(dmXuatKhoRawData);
                }

                context.DM_Xuat_Kho.Remove(dmXuatKho);
                await context.SaveChangesAsync();
                return new Response(true, $"{dmXuatKho.So_Phieu_Xuat_Kho} is deleted successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred deleting DM_Nhap_Kho, {ex.Message}");
            }
        }

        public async Task<IEnumerable<DM_Xuat_Kho>> GetAllAsync()
        {
            try
            {
                var list = await context.DM_Xuat_Kho.AsNoTracking().ToListAsync();
                return list is not null ? list : null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred retrieving DM_Xuat_Kho, {ex.Message}");
            }
        }

        public async Task<DM_Xuat_Kho> GetByIdAsync(Guid id)
        {
            try
            {
                var dmXuatKho = await context.DM_Xuat_Kho.Where(nk => nk.Id == id).SingleOrDefaultAsync();
                return dmXuatKho is not null ? dmXuatKho : null!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred retrieving DM_Xuat_Kho, {ex.Message}");
            }
        }
    }
}
