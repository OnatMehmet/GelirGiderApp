namespace GelirGiderApp.Models.Entities
{
    public class Files : BaseEntity
    {
        public string FileName { get; set; }  // Dosya Adı
        public string FilePath { get; set; }  // Dosya Yolu
        public DateTime UploadDate { get; set; }  // Yükleme Tarihi
        public Guid PatientId { get; set; }  // Hasta Kimliği (Foreign Key)
        public Patient Patient { get; set; }  // İlgili Hasta
    }

}
