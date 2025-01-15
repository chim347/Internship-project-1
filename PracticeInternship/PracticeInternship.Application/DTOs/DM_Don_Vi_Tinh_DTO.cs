using System.ComponentModel.DataAnnotations;

namespace PracticeInternship.Application.DTOs
{
    /*public record DM_Don_Vi_Tinh_DTO(int id, [Required] string Ten_Don_Vi_Tinh, string Ghi_Chu);*/
    public class DM_Don_Vi_Tinh_DTO
    {
        [Required]
        public string? Ten_Don_Vi_Tinh { get; set; }

        public string? Ghi_Chu { get; set; }
    }
}
