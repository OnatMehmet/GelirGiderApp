namespace GelirGiderApp.Models.Entities
{
    public class Diagnosis : BaseEntity
    {
        public string Code { get; set; }  // Tanı Kimliği
        public string Name { get; set; }  // Tanı Kimliği
        public Guid PatientId { get; set; }  // Hasta Kimliği (Foreign Key)
        public Patient Patient { get; set; }  // İlgili Hasta
    }

}
