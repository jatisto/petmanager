namespace pet_manager.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string NotificationText { get; set; }
        public bool IsRead { get; set; }
        public Owner User { get; set; }
        public string UserId { get; set; }
        public int PetId { get; set; }
    }
}