using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using pet_manager.Infrastructure;
using static ClientNotifications.Helpers.NotificationHelper;

namespace pet_manager.Controllers{
    public class BaseController : Controller
    {
        public void Alert(string message, NotificationPosition position, NotificationType notificationType){
            var msg = "toastr."+notificationType+"('"+message+"')";
            TempData["notification"]=msg;

            var positions = new ClientNotifications.Helpers.NotificationHelper().position();

            TempData["options"]=JsonConvert.SerializeObject(new{positionClass=positions[position]});
        }
    }
}