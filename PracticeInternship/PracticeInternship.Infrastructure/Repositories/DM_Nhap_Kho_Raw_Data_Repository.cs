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
    public class DM_Nhap_Kho_Raw_Data_Repository : Interface_DM_Nhap_Kho_Raw_Data
    {
        private readonly PracticeInternshipDbContext context;

        public DM_Nhap_Kho_Raw_Data_Repository(PracticeInternshipDbContext _context)
        {
            context = _context;
        }

        public async Task<Response> CreateAsync(DM_Nhap_Kho_Raw_Data_DTO_Version_2 entity)
        {
            try
            {
                // kiểm tra id của phiếu nhập có tồn tại ko
                var nhapKhoIdExist = await context.DM_Nhap_Kho.FirstOrDefaultAsync(x => x.Id == Guid.Parse(entity.Nhap_Kho_Id!));
                if (nhapKhoIdExist == null)
                    return new Response(false, "Phiếu này có keyId không tồn tại.");

                // check null
                if (string.IsNullOrEmpty(entity.Ten_San_Pham) || string.IsNullOrEmpty(entity.Ma_San_Pham))
                {
                    return new Response(false, $"Name or Code of product is not Empty!!");
                }
                if (string.IsNullOrEmpty(entity.Loai_San_Pham_Id))
                    return new Response(false, "Loại sản phẩm không được rỗng.");
                if (string.IsNullOrEmpty(entity.Don_Vi_Tinh_Id))
                    return new Response(false, "Đơn vịt tính không được rỗng.");
                if (await context.DM_San_Pham.AnyAsync(x => x.Ma_San_Pham == entity.Ma_San_Pham))
                    return new Response(false, "Mã sản phẩm đã tồn tại.");

                var sanPham = new DM_San_Pham
                {
                    Ma_San_Pham = entity.Ma_San_Pham,
                    Ten_San_Pham = entity.Ten_San_Pham,
                    Ghi_Chu = "",
                    Loai_San_Pham_Id = Guid.Parse(entity.Loai_San_Pham_Id),
                    Don_Vi_Tinh_Id = Guid.Parse(entity.Don_Vi_Tinh_Id),
                };
                context.DM_San_Pham.Add(sanPham);

                var nhapKhoRawData = new DM_Nhap_Kho_Raw_Data
                {
                    Nhap_Kho_Id = nhapKhoIdExist.Id,
                    San_Pham_Id = sanPham.Id,
                    SL_Nhap = entity.SL_Nhap,
                    Don_Gia_Nhap = entity.Don_Gia_Nhap,
                };
                context.DM_Nhap_Kho_Raw_Data.Add(nhapKhoRawData);

                await context.SaveChangesAsync();
                return new Response(true, $"{entity.Ten_San_Pham} added to database successfully");
            }
            catch (Exception ex)
            {
                return new Response(false, $"Error occurred adding new DM_San_Pham, ${ex.Message}");
            }
        }

        public async Task<Response> DeleteAsync(DM_Nhap_Kho_Raw_Data entity)
        {
            try
            {
                var dmNhapKhoRawData = await context.DM_Nhap_Kho_Raw_Data.Where(nk => nk.Id == entity.Id).SingleOrDefaultAsync();
                if (dmNhapKhoRawData == null)
                {
                    return new Response(false, $"{entity.Id} not found");
                }
                context.DM_Nhap_Kho_Raw_Data.Remove(dmNhapKhoRawData);

                var dmSanPham = await context.DM_San_Pham.Where(sp => sp.Id == dmNhapKhoRawData.San_Pham_Id).SingleOrDefaultAsync();
                if (dmSanPham == null)
                {
                    return new Response(false, $"Not found DM_San_Pham by {dmNhapKhoRawData.San_Pham_Id}");
                }
                context.DM_San_Pham.Remove(dmSanPham);
                await context.SaveChangesAsync();
                return new Response(true, $"Deleted successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred deleting DM_Nhap_Kho, {ex.Message}");
            }
        }

        public async Task<Response> UpdateAsync(DM_Nhap_Kho_Raw_Data_DTO_Edit entity)
        {
            try
            {
                var nhapKhoRawData = await context.DM_Nhap_Kho_Raw_Data
                                        .Where(nkr => nkr.Id == Guid.Parse(entity.Id!))
                                        .SingleOrDefaultAsync();
                if (nhapKhoRawData == null)
                {
                    return new Response(false, $"DM_Nhap_Kho_Raw_Data_Id not found");
                }

                nhapKhoRawData.SL_Nhap = entity.SL_Nhap;
                nhapKhoRawData.Don_Gia_Nhap = entity.Don_Gia_Nhap;

                context.DM_Nhap_Kho_Raw_Data.Update(nhapKhoRawData);
                await context.SaveChangesAsync();

                return new Response(true, $"Update successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred updating existing DM_Nhap_Kho_Raw_Data, {ex.Message}");
            }
        }
    }
}
