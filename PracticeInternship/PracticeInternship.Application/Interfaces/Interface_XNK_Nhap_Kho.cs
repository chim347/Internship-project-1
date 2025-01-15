using PracticeInternship.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInternship.Application.Interfaces
{
    public interface Interface_XNK_Nhap_Kho 
    {
        Task<bool> GetByIdAsync(string id);
        Task<IEnumerable<XNK_Nhap_Kho>> GetAllAsync();
    }
}
