namespace GelirGiderApp.Models.Entities
{
    public class Page
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } // Sayfa Adı
        public string Url { get; set; }  // Sayfa URL'si
        public Guid? ParentId { get; set; } // Eğer alt menüyse üst menü ID'si
        public bool IsActive { get; set; } = true; // Sayfa aktif mi?
    }
}
