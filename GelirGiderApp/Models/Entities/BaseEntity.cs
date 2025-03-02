namespace GelirGiderApp.Models.Entities
{
    public class BaseEntity<T>
    {
        private readonly ApplicationDbContext _context;

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public string? UpdatedBy { get; set; }
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}
