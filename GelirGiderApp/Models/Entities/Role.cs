namespace GelirGiderApp.Models.Entities
{
    public class Role : BaseEntity<Role>
    {
        public required string Name { get; set; }  // Rol adı (Admin, Doktor vb.)

    }
}
