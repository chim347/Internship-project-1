using PracticeInternship.Domain.Common;

namespace PracticeInternship.Domain.Entities
{
    public class DM_Loai_San_Pham : BaseEntities
    {
        public string? Ma_LSP { get; set; }
        public string? Ten_LSP { get; set; }
        public string? Ghi_Chu { get; set; }

        // relationship
        public virtual IList<DM_San_Pham> DM_San_Pham { get; set; } = null!;
    }
}
