using System.Collections.Generic;
using pet_manager.Models;

namespace pet_manager.Repository
{
    public interface INotificationRepository
    {
        void CreateNotification(int petId, string userId);
        void UpdateNotificationReadStatusToTrue(int Id);
        List<Notification> GetUserNotifications(string userId);
    }
}