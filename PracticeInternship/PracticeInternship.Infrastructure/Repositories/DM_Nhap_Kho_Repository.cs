using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PracticeInternship.Application.DTOs;
using PracticeInternship.Application.Interfaces;
using PracticeInternship.Application.Responses;
using PracticeInternship.Domain.Entities;
using PracticeInternship.Infrastructure.Data;
using System.Collections;

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

        public async Task<DM_Nhap_Kho_Detail_Response> GetDetailOfNhapKhoById(Guid id)
        {
            var nhapKho = await context.DM_Nhap_Kho.Where(nk => nk.Id == id).SingleOrDefaultAsync();
            if (nhapKho == null)
            {
                throw new Exception($"Not Found Nhap Kho Id {id}");
            }

            var response = new DM_Nhap_Kho_Detail_Response
            {
                Id = nhapKho.Id.ToString(),
                So_Phieu_Nhap_Kho = nhapKho.So_Phieu_Nhap_Kho,
                Kho_Id = nhapKho.Kho_Id.ToString(),
                Ten_Kho = await context.DM_Kho.Where(k=>k.Id == nhapKho.Kho_Id).Select(k => k.Ten_Kho).SingleOrDefaultAsync(),
                NCC_Id = nhapKho.NCC_Id.ToString(),
                Ngay_Nhap_kho = nhapKho.Ngay_Nhap_kho,
                Ghi_Chu = nhapKho.Ghi_Chu,
                List_DM_Nhap_Kho_Raw_Data_Response = await FindInfoRawData(nhapKho.Id.ToString())
            };

            return response;
        }
        private async Task<IList<DM_Nhap_Kho_Raw_Data_Response>> FindInfoRawData(string id)
        {
            var listNhapKhoDetail = await context.DM_Nhap_Kho_Raw_Data.Where(nkd => nkd.Nhap_Kho_Id == Guid.Parse(id)).ToListAsync();
            var listResponse = new List<DM_Nhap_Kho_Raw_Data_Response>();
            foreach (var item in listNhapKhoDetail)
            {
                var response = new DM_Nhap_Kho_Raw_Data_Response();
                response.Id = item.Id.ToString();
                response.Nhap_Kho_Id = item.Nhap_Kho_Id.ToString();
                response.San_Pham_Id = item.San_Pham_Id.ToString();
                response.SL_Nhap = item.SL_Nhap;
                response.Don_Gia_Nhap = item.Don_Gia_Nhap;
                var sanPhamExist = await context.DM_San_Pham.Where(sp => sp.Id == item.San_Pham_Id).SingleOrDefaultAsync();
                response.Ma_San_Pham = sanPhamExist!.Ma_San_Pham;
                response.Ten_San_Pham = sanPhamExist!.Ten_San_Pham;
                response.Ten_Don_Vi_Tinh = await context.DM_Don_Vi_Tinh.Where(dvt => dvt.Id == sanPhamExist!.Don_Vi_Tinh_Id).Select(dvt => dvt.Ten_Don_Vi_Tinh).SingleOrDefaultAsync();
                listResponse.Add(response);
            }

            return listResponse;
        }

        public async Task<Response> UpdateAsync(DM_Nhap_Kho entity)
        {
            try
            {
                var nhapKho = await context.DM_Nhap_Kho.Where(nk => nk.Id == entity.Id).AsNoTracking().SingleOrDefaultAsync();
                if (nhapKho == null)
                {
                    return new Response(false, $"{entity.So_Phieu_Nhap_Kho} not found");
                }

                // Detach entity hiện tại nếu nó đang được tracking
                context.Entry(nhapKho).State = EntityState.Detached;

                // check null
                if (string.IsNullOrEmpty(entity.So_Phieu_Nhap_Kho))
                    return new Response(false, "Số phiếu nhập không được rỗng.");
                if (string.IsNullOrEmpty(entity.Kho_Id.ToString()))
                    return new Response(false, "Kho không được rỗng.");
                if (string.IsNullOrEmpty(entity.NCC_Id.ToString()))
                    return new Response(false, "Nhà cung cấp không được rỗng.");
                if (entity.Ngay_Nhap_kho == default)
                    return new Response(false, "Ngày nhập kho không được rỗng.");

                // check So_Phieu_Nhap_Kho is already exist
                if (!string.Equals(nhapKho.So_Phieu_Nhap_Kho, entity.So_Phieu_Nhap_Kho, StringComparison.OrdinalIgnoreCase))
                {
                    if (await context.DM_Nhap_Kho.AnyAsync(x => x.So_Phieu_Nhap_Kho == entity.So_Phieu_Nhap_Kho))
                        return new Response(false, "Số phiếu nhập đã tồn tại.");
                }

                var xnk = new XNK_Nhap_Kho
                {
                    Nhap_Kho_Id = entity.Id,
                    So_Phieu_Nhap_Kho = entity.So_Phieu_Nhap_Kho,
                    NCC_Id = entity.NCC_Id,
                    Kho_Id = entity.Kho_Id,
                    Ngay_Nhap_kho = entity.Ngay_Nhap_kho,
                    Ghi_Chu = entity.Ghi_Chu,
                };

                context.XNK_Nhap_Kho.Add(xnk);
                await context.SaveChangesAsync();

                return new Response(true, $"{entity.So_Phieu_Nhap_Kho} is update successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred updating existing DM_San_Pham, {ex.Message}");
            }
        }
    }
}
