namespace GelirGiderApp.Models.Entities
{
    public class BaseEntity<T>
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public Guid UpdatedBy { get; set; }
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}
