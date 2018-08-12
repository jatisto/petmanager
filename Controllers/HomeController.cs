using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ClientNotifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using pet_manager.Models;
using pet_manager.Repository;
using static ClientNotifications.Helpers.NotificationHelper;

namespace pet_manager.Controllers
{
    public class HomeController : Controller
    {
        private IPetRepository _petRepository;
        private UserManager<Owner> _userManager;
        public HomeController(IPetRepository petRepository,
                                UserManager<Owner> userManager,
                                INotificationRepository notificationRepository)
        {
            _petRepository = petRepository;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(_petRepository.GetAllPets());
        }

        [Authorize]
        public IActionResult MyPets(){
            List<Pet> pets = _petRepository.GetUserPets(_userManager.GetUserId(HttpContext.User));

            return View("_PetList",pets);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View(nameof(About));
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
