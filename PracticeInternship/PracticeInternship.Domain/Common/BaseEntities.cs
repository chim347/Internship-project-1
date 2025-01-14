namespace PracticeInternship.Domain.Common
{
    public abstract class BaseEntities
    {
        public Guid Ma_LSP { get; set; }
        public BaseEntities() => Ma_LSP = Guid.NewGuid();
    }
}
