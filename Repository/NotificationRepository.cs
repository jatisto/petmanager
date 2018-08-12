using System.Collections.Generic;
using System.Linq;
using pet_manager.Data;
using pet_manager.Models;

namespace pet_manager.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void CreateNotification(int petId, string userId)
        {
            var pet = _context.Pets.FirstOrDefault(p=>p.Id==petId);
            var userName = _context.Users.FirstOrDefault(u=>u.Id.Equals(pet.OwnerId)).Name;

            Notification notification = new Notification{
                PetId = petId, UserId = userId, IsRead = false, 
                NotificationText = userName+" is selling their pet "+
                                    pet.Name +".Please click to check"
            };
            if(!_context.Notifications.Any(n=>n.PetId==petId && n.UserId.Equals(userId) && n.IsRead==false)){
                _context.Notifications.Add(notification);
                _context.SaveChanges();
            }
        }

        public List<Notification> GetUserNotifications(string userId)
        {
            return _context.Notifications
                            .Where(n=>n.UserId.Equals(userId) && n.IsRead==false)
                            .ToList();
        }

        public void UpdateNotificationReadStatusToTrue(int Id)
        {
            var notification = _context.Notifications.FirstOrDefault(n=>n.Id==Id);
            notification.IsRead = true;
            _context.Notifications.Update(notification);
            _context.SaveChanges();
        }
    }
}