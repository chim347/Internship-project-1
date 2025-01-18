using PracticeInternship.Application.DTOs;
using PracticeInternship.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInternship.Application.Interfaces
{
    public interface Interface_Report_Xuat_Nhap_Ton
    {
        Task<IEnumerable<Report_Xuat_Nhap_Ton_DTO>> GetReportXuatNhapSPAynsc(string startDateInput, string endDateInput);
    }
}
