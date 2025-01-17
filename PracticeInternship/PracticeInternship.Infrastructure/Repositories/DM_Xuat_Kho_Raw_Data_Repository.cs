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
    public class DM_Xuat_Kho_Raw_Data_Repository : Interface_DM_Xuat_Kho_Raw_Data
    {
        private readonly PracticeInternshipDbContext context;

        public DM_Xuat_Kho_Raw_Data_Repository(PracticeInternshipDbContext _context)
        {
            context = _context;
        }

        public async Task<Response> CreateAsync(DM_Xuat_Kho_Raw_Data_DTO_Version_2 entity)
        {
            try
            {
                // kiểm tra id của phiếu nhập có tồn tại ko
                var xuatKhoIdExist = await context.DM_Xuat_Kho.FirstOrDefaultAsync(x => x.Id == Guid.Parse(entity.Xuat_Kho_Id!));
                if (xuatKhoIdExist == null)
                    return new Response(false, "Phiếu này có keyId không tồn tại.");
                var sanPhamExist = await context.DM_San_Pham.FirstOrDefaultAsync(x => x.Id == Guid.Parse(entity.San_Pham_Id!));
                if (sanPhamExist == null)
                {
                    return new Response(false, $"Mã sản phẩm không được rỗng.");
                }

                var xuatKhoRawData = new DM_Xuat_Kho_Raw_Data
                {
                    Xuat_Kho_Id = xuatKhoIdExist.Id,
                    San_Pham_Id = sanPhamExist.Id,
                    SL_Xuat = entity.SL_Xuat,
                    Don_Gia_Xuat = entity.Don_Gia_Xuat,
                };
                context.DM_Xuat_Kho_Raw_Data.Add(xuatKhoRawData);

                await context.SaveChangesAsync();
                return new Response(true, $"added to database successfully");
            }
            catch (Exception ex)
            {
                return new Response(false, $"Error occurred adding new DM_Xuat_Kho_Raw_Data, ${ex.Message}");
            }
        }

        public async Task<Response> DeleteAsync(DM_Xuat_Kho_Raw_Data entity)
        {
            try
            {
                var dmXuatKhoRawData = await context.DM_Xuat_Kho_Raw_Data.Where(nk => nk.Id == entity.Id).SingleOrDefaultAsync();
                if (dmXuatKhoRawData == null)
                {
                    return new Response(false, $"{entity.Id} not found");
                }
                context.DM_Xuat_Kho_Raw_Data.Remove(dmXuatKhoRawData);

                await context.SaveChangesAsync();
                return new Response(true, $"Deleted successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred deleting DM_Xuat_Kho_Raw_Data, {ex.Message}");
            }
        }

        public async Task<Response> UpdateAsync(DM_Xuat_Kho_Raw_Data_DTO_Edit entity)
        {
            try
            {
                var xuatKhoRawData = await context.DM_Xuat_Kho_Raw_Data
                                        .Where(xkr => xkr.Id == Guid.Parse(entity.Id!))
                                        .SingleOrDefaultAsync();
                if (xuatKhoRawData == null)
                {
                    return new Response(false, $"DM_Xuat_Kho_Raw_Data_Id not found");
                }

                xuatKhoRawData.SL_Xuat = entity.SL_Xuat;
                xuatKhoRawData.Don_Gia_Xuat = entity.Don_Gia_Xuat;

                context.DM_Xuat_Kho_Raw_Data.Update(xuatKhoRawData);
                await context.SaveChangesAsync();

                return new Response(true, $"Update successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred updating existing DM_Xuat_Kho_Raw_Data, {ex.Message}");
            }
        }
    }
}
