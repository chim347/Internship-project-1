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

        #region GetDetailOfXuatKhoById
        public async Task<DM_Xuat_Kho_Detail_Response> GetDetailOfXuatKhoById(Guid id)
        {
            var xuatKho = await context.DM_Xuat_Kho.Where(nk => nk.Id == id).SingleOrDefaultAsync();
            if (xuatKho == null)
            {
                throw new Exception($"Not Found Xuat Kho Id {id}");
            }

            var response = new DM_Xuat_Kho_Detail_Response
            {
                Id = xuatKho.Id.ToString(),
                So_Phieu_Xuat_Kho = xuatKho.So_Phieu_Xuat_Kho,
                Kho_Id = xuatKho.Kho_Id.ToString(),
                Ten_Kho = await context.DM_Kho.Where(k => k.Id == xuatKho.Kho_Id).Select(k => k.Ten_Kho).SingleOrDefaultAsync(),
                Ngay_Xuat_kho = xuatKho.Ngay_Xuat_Kho,
                Ghi_Chu = xuatKho.Ghi_Chu,
                List_DM_Xuat_Kho_Raw_Data_Response = await FindInfoRawData(xuatKho.Id.ToString())
            };

            return response;
        }
        private async Task<IList<DM_Xuat_Kho_Raw_Data_Response>> FindInfoRawData(string id)
        {
            var listNhapKhoDetail = await context.DM_Xuat_Kho_Raw_Data.Where(nkd => nkd.Xuat_Kho_Id == Guid.Parse(id)).ToListAsync();
            var listResponse = new List<DM_Xuat_Kho_Raw_Data_Response>();
            foreach (var item in listNhapKhoDetail)
            {
                var response = new DM_Xuat_Kho_Raw_Data_Response();
                response.Id = item.Id.ToString();
                response.Xuat_Kho_Id = item.Xuat_Kho_Id.ToString();
                response.San_Pham_Id = item.San_Pham_Id.ToString();
                response.SL_Xuat = item.SL_Xuat;
                response.Don_Gia_Xuat = item.Don_Gia_Xuat;
                var sanPhamExist = await context.DM_San_Pham.Where(sp => sp.Id == item.San_Pham_Id).SingleOrDefaultAsync();
                response.Ma_San_Pham = sanPhamExist!.Ma_San_Pham;
                response.Ten_San_Pham = sanPhamExist!.Ten_San_Pham;
                response.Ten_Don_Vi_Tinh = await context.DM_Don_Vi_Tinh.Where(dvt => dvt.Id == sanPhamExist!.Don_Vi_Tinh_Id).Select(dvt => dvt.Ten_Don_Vi_Tinh).SingleOrDefaultAsync();
                listResponse.Add(response);
            }

            return listResponse;
        }
        #endregion

        public async Task<Response> UpdateAsync(DM_Xuat_Kho entity)
        {
            try
            {
                var xuatKho = await context.DM_Xuat_Kho.Where(nk => nk.Id == entity.Id).AsNoTracking().SingleOrDefaultAsync();
                if (xuatKho == null)
                {
                    return new Response(false, $"{entity.So_Phieu_Xuat_Kho} not found");
                }

                // Detach entity hiện tại nếu nó đang được tracking
                context.Entry(xuatKho).State = EntityState.Detached;

                // check null
                if (string.IsNullOrEmpty(entity.So_Phieu_Xuat_Kho))
                    return new Response(false, "Số phiếu xuất không được rỗng.");
                if (string.IsNullOrEmpty(entity.Kho_Id.ToString()))
                    return new Response(false, "Kho không được rỗng.");
                if (entity.Ngay_Xuat_Kho == default)
                    return new Response(false, "Ngày xuất kho không được rỗng.");

                // check So_Phieu_Xuat_Kho is already exist
                if (!string.Equals(xuatKho.So_Phieu_Xuat_Kho, entity.So_Phieu_Xuat_Kho, StringComparison.OrdinalIgnoreCase))
                {
                    if (await context.DM_Xuat_Kho.AnyAsync(x => x.So_Phieu_Xuat_Kho == entity.So_Phieu_Xuat_Kho))
                        return new Response(false, "Số phiếu xuất đã tồn tại.");
                }

                var xnk = new XNK_Xuat_Kho
                {
                    Xuat_Kho_Id = entity.Id,
                    So_Phieu_Xuat_Kho = entity.So_Phieu_Xuat_Kho,
                    Kho_Id = entity.Kho_Id,
                    Ngay_Xuat_kho = entity.Ngay_Xuat_Kho,
                    Ghi_Chu = entity.Ghi_Chu,
                };

                context.XNK_Xuat_Kho.Add(xnk);
                await context.SaveChangesAsync();

                return new Response(true, $"{entity.So_Phieu_Xuat_Kho} is update successfully");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred updating existing DM_San_Pham, {ex.Message}");
            }
        }

        public async Task<IEnumerable<DM_Xuat_Kho_Search_Ngay_Nhap_kho_Response>> GetAllDetailOfXuatKhoBySearchDate(string startDateInput, string endDateInput)
        {
            DateTime? startDate = string.IsNullOrWhiteSpace(startDateInput) ? null
                                        : DateTime.TryParse(startDateInput, out var parsedStartDate)
                                        ? parsedStartDate
                                        : throw new ArgumentException("Invalid start date format");

            DateTime? endDate = string.IsNullOrWhiteSpace(endDateInput) ? null
                                        : DateTime.TryParse(endDateInput, out var parsedEndDate)
                                        ? parsedEndDate
                                        : throw new ArgumentException("Invalid end date format");

            var response = await (from nk in context.DM_Xuat_Kho
                                  join xk_raw_data in context.DM_Xuat_Kho_Raw_Data on nk.Id equals xk_raw_data.Xuat_Kho_Id
                                  join sp in context.DM_San_Pham on xk_raw_data.San_Pham_Id equals sp.Id
                                  where (!startDate.HasValue || nk.Ngay_Xuat_Kho >= startDate.Value)
                                      && (!endDate.HasValue || nk.Ngay_Xuat_Kho <= endDate.Value)
                                  orderby nk.Ngay_Xuat_Kho descending
                                  select new DM_Xuat_Kho_Search_Ngay_Nhap_kho_Response
                                  {
                                      Ngay_Xuat_Kho = nk.Ngay_Xuat_Kho,
                                      So_Phieu_Xuat_Kho = nk.So_Phieu_Xuat_Kho,
                                      Ma_San_Pham = sp.Ma_San_Pham,
                                      Ten_San_Pham = sp.Ten_San_Pham,
                                      SL_Xuat = xk_raw_data.SL_Xuat,
                                      Don_Gia_Xuat = xk_raw_data.Don_Gia_Xuat,
                                      Tri_Gia = xk_raw_data.SL_Xuat * xk_raw_data.Don_Gia_Xuat
                                  }).ToListAsync();

            return response;
        }
    }
}
