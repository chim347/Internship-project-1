using Microsoft.EntityFrameworkCore;
using PracticeInternship.Application.DTOs;
using PracticeInternship.Application.Interfaces;
using PracticeInternship.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInternship.Infrastructure.Repositories
{
    public class Report_Xuat_Nhap_Ton_Repository : Interface_Report_Xuat_Nhap_Ton
    {
        private readonly PracticeInternshipDbContext context;

        public Report_Xuat_Nhap_Ton_Repository(PracticeInternshipDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<Report_Xuat_Nhap_Ton_DTO>> GetReportXuatNhapSPAynsc(string startDateInput, string endDateInput)
        {
            DateTime? startDate = string.IsNullOrWhiteSpace(startDateInput) ? null
                                        : DateTime.TryParse(startDateInput, out var parsedStartDate)
                                        ? parsedStartDate
                                        : throw new ArgumentException("Invalid start date format");

            DateTime? endDate = string.IsNullOrWhiteSpace(endDateInput) ? null
                                        : DateTime.TryParse(endDateInput, out var parsedEndDate)
                                        ? parsedEndDate
                                        : throw new ArgumentException("Invalid end date format");
            var sanPhamList = await context.DM_San_Pham.ToListAsync();
            var response = new List<Report_Xuat_Nhap_Ton_DTO>();
            foreach (var sp in sanPhamList)
            {
                // Số lượng đầu kỳ: Tính tổng nhập và xuất trước ngày bắt đầu kỳ
                var sumNhapKho_DauKy = await (from nk_raw_data in context.DM_Nhap_Kho_Raw_Data
                                              join nk in context.DM_Nhap_Kho on nk_raw_data.Nhap_Kho_Id equals nk.Id
                                              where nk.Ngay_Nhap_kho < startDate && nk_raw_data.San_Pham_Id.Equals(sp.Id)
                                              select nk_raw_data.SL_Nhap
                                        ).SumAsync();

                var sumXuatKho_DauKy = await (from xk_raw_data in context.DM_Xuat_Kho_Raw_Data
                                              join xk in context.DM_Xuat_Kho on xk_raw_data.Xuat_Kho_Id equals xk.Id
                                              where xk.Ngay_Xuat_Kho < startDate && xk_raw_data.San_Pham_Id.Equals(sp.Id)
                                              select xk_raw_data.SL_Xuat
                                        ).SumAsync();
                var sumDauKy = sumNhapKho_DauKy - sumXuatKho_DauKy;

                // Số lượng trong kỳ: Tính tổng nhập trong kỳ
                var sumNhapKho_TrongKy = await (from nk_raw_data in context.DM_Nhap_Kho_Raw_Data
                                                join nk in context.DM_Nhap_Kho on nk_raw_data.Nhap_Kho_Id equals nk.Id
                                                where nk.Ngay_Nhap_kho <= endDate
                                                        && nk.Ngay_Nhap_kho >= startDate
                                                        && nk_raw_data.San_Pham_Id.Equals(sp.Id)
                                                select nk_raw_data.SL_Nhap
                                       ).SumAsync();

                // Số lượng trong kỳ: Tính tổng xuất trong kỳ
                var sumXuatKho_TrongKy = await (from xk_raw_data in context.DM_Xuat_Kho_Raw_Data
                                                join xk in context.DM_Xuat_Kho on xk_raw_data.Xuat_Kho_Id equals xk.Id
                                                where xk.Ngay_Xuat_Kho >= startDate
                                                          && xk.Ngay_Xuat_Kho <= endDate
                                                          && xk_raw_data.San_Pham_Id.Equals(sp.Id)
                                                select xk_raw_data.SL_Xuat
                                        ).SumAsync();

                // Số lượng cuối kỳ: Tính tổng nhập - xuất đến ngày kết thúc kỳ
                var sumNhapKho_CuoiKy = await (from nk_raw_data in context.DM_Nhap_Kho_Raw_Data
                                               join nk in context.DM_Nhap_Kho on nk_raw_data.Nhap_Kho_Id equals nk.Id
                                               where nk.Ngay_Nhap_kho <= endDate && nk_raw_data.San_Pham_Id.Equals(sp.Id)
                                               select nk_raw_data.SL_Nhap
                                        ).SumAsync();

                var sumXuatKho_CuoiKy = await (from xk_raw_data in context.DM_Xuat_Kho_Raw_Data
                                               join xk in context.DM_Xuat_Kho on xk_raw_data.Xuat_Kho_Id equals xk.Id
                                               where xk.Ngay_Xuat_Kho <= endDate && xk_raw_data.San_Pham_Id.Equals(sp.Id)
                                               select xk_raw_data.SL_Xuat
                                        ).SumAsync();

                var sumCuoiKy = sumNhapKho_CuoiKy - sumXuatKho_CuoiKy;
                var report = new Report_Xuat_Nhap_Ton_DTO
                {
                    Ma_San_Pham = sp.Ma_San_Pham,
                    Ten_San_Pham = sp.Ten_San_Pham,
                    SL_Dau_Ky = sumDauKy,
                    SL_Nhap = sumNhapKho_TrongKy,
                    SL_Xuat = sumXuatKho_TrongKy,
                    SL_Cuoi_ky = sumCuoiKy,
                };
                response.Add(report);
            }
            return response;
        }
    }
}
