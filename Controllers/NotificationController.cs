using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pet_manager.Models;
using pet_manager.Repository;

namespace pet_manager.Controllers
{
    public class NotificationController:Controller
    {
        private UserManager<Owner> _userManager;
        private INotificationRepository _notificationRepository;

        public NotificationController(UserManager<Owner> userManager,
                                        INotificationRepository notificationRepository)
        {
            _userManager = userManager;
            _notificationRepository = notificationRepository;
        }

        public IActionResult Index(){
            var userId = _userManager.GetUserId(HttpContext.User);
            var notifications = _notificationRepository.GetUserNotifications(userId);
            
            if(notifications==null)
                return Content("No notifications");

            return Ok(notifications.Count);
        }

        [Authorize]
        public IActionResult ReadNotifications(){
            var userId = _userManager.GetUserId(HttpContext.User);

            var notifications = _notificationRepository.GetUserNotifications(userId);

            return PartialView("_DisplayNotifications",notifications);
        }
    }
}