using PracticeInternship.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInternship.Application.DTOs.Conversions
{
    public static class DM_Don_Vi_Tinh_Conversion
    {
        public static DM_Don_Vi_Tinh ToEntity(DM_Don_Vi_Tinh_DTO don_Vi_Tinh) => new
            DM_Don_Vi_Tinh
        {
            Ten_Don_Vi_Tinh = don_Vi_Tinh.Ten_Don_Vi_Tinh,
            Ghi_Chu = don_Vi_Tinh.Ghi_Chu
        };

        public static (DM_Don_Vi_Tinh_DTO?, IEnumerable<DM_Don_Vi_Tinh_DTO>?) FromEntity(
            DM_Don_Vi_Tinh don_Vi_Tinh,
            IEnumerable<DM_Don_Vi_Tinh> dM_Don_Vi_Tinhs)
        {
            // return single
            if (don_Vi_Tinh is not null || dM_Don_Vi_Tinhs is null)
            {
                var single_Don_Vi_Tinh = new DM_Don_Vi_Tinh_DTO
                {
                    Ten_Don_Vi_Tinh = don_Vi_Tinh.Ten_Don_Vi_Tinh!,
                    Ghi_Chu = don_Vi_Tinh.Ghi_Chu
                };
                return (single_Don_Vi_Tinh, null);
            }

            // return list
            if (don_Vi_Tinh is null || dM_Don_Vi_Tinhs is not null)
            {
                var _don_Vi_Tinhs = dM_Don_Vi_Tinhs.Select(d => new DM_Don_Vi_Tinh_DTO
                {
                    Ten_Don_Vi_Tinh = d.Ten_Don_Vi_Tinh!,
                    Ghi_Chu = d.Ghi_Chu
                }).ToList();
                return (null, _don_Vi_Tinhs);
            }

            return (null, null);
        }
    }
}
