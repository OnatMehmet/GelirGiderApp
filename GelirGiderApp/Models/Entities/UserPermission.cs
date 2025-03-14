namespace GelirGiderApp.Models.Entities
{
    public class UserPermission
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; } // Kullanıcıya bağlı olacak
        public string PageName { get; set; } // Hangi sayfa için izin
        public bool CanView { get; set; } = true;
        public bool CanEdit { get; set; } = false;
        public bool CanDelete { get; set; } = false;
        public bool CanCreate { get; set; } = false;
    }
}
