namespace GelirGiderApp.Models.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }  = Guid.NewGuid();
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}
