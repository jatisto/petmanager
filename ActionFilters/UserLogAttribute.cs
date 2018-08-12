using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using pet_manager.Data;
using pet_manager.Models;

namespace pet_manager.ActionFilters
{
    public class UserLogAttribute : ActionFilterAttribute {
        public UserLogAttribute(string description, ApplicationDbContext context)
        {
            _context = context;
            _description = description;
        }

        public ApplicationDbContext _context { get; private set; }
        public string _description { get; private set; }

        public override void OnActionExecuted(ActionExecutedContext context){
            var userName = context.HttpContext.User.Identity.Name;
            var userId = _context.Users.FirstOrDefault(u=>u.UserName.Equals(userName)).Id;
            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];

            var userLog = new UserLog{
                UserName = userName,
                UserId = userId.ToString(),
                Controller = controllerName.ToString(),
                Action = actionName.ToString(),
                Description = _description
            };

            _context.UserLogs.Add(userLog);
            _context.SaveChanges();

            base.OnActionExecuted(context);
        }
    }
}