namespace GelirGiderApp.Models.Entities
{
    public class Note :BaseEntity
    {
        public  Guid PatientId { get; set; }
        public Patient? Patient { get; set; }        
        public  Guid UsertId { get; set; }
        public User? User { get; set; }
    }
}
