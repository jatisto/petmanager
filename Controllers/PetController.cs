using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ClientNotifications;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using pet_manager.BackgroundJob;
using pet_manager.Infrastructure;
using pet_manager.Models;
using pet_manager.Repository;
using pet_manager.ActionFilters;
using static ClientNotifications.Helpers.NotificationHelper;

namespace pet_manager.Controllers
{
    [Authorize]
    public class PetController: Controller
    {
        private IPetRepository _petRepository;
        private UserManager<Owner> _userManager;
        private IUpdatePetRepository _updatePetRepository;
        private IWatchlistRepository _watchlistRepository;
        private INotificationRepository _notificationRepository;
        private IHubContext<SignalServer> _hubContext;
        private IClientNotification _clientNotification;

        public PetController(IPetRepository petRepository,
                            UserManager<Owner> userManager,
                            IUpdatePetRepository updatePetRepository,
                            IWatchlistRepository watchlistRepository,
                            INotificationRepository notificationRepository,
                            IHubContext<SignalServer> hubContext,
                            IClientNotification clientNotification)
        {
            _petRepository = petRepository;
            _userManager = userManager;
            _updatePetRepository = updatePetRepository;
            _watchlistRepository = watchlistRepository;
            _notificationRepository = notificationRepository;
            _hubContext = hubContext;
            _clientNotification = clientNotification;
        }
        
        [TypeFilter(typeof(UserLogAttribute),Arguments = new Object[]{"From Index Action"})]
        public IActionResult Index(){
            return View(_petRepository.GetAllPets());
        }

        public IActionResult GetSellingPets(){
            return View("_PetList",_petRepository.GetSellingPets());
        }
        public IActionResult Detail(int Id, int notificationId=0){
            List<History> histories = _petRepository.GetPetWithHistories(Id);
            ViewBag.histories = histories;

            if(notificationId>0)
                _notificationRepository.UpdateNotificationReadStatusToTrue(notificationId);
            
            return View(_petRepository.GetPet(Id));
        }

        public IActionResult New(){
            return View(new Pet());
        }

        [HttpPost]
        [ModelValidator]
        [TypeFilter(typeof(UserLogAttribute),Arguments = new Object[]{"Pet Creation"})]
        
        public IActionResult New(Pet pet){
            try{
                pet.OwnerId = _userManager.GetUserId(HttpContext.User);

                if(TempData["fileName"]!=null){
                    pet.ImageUrl = TempData["fileName"].ToString();
                }

                _petRepository.Save(pet);

                TempData["fileName"]="";

                _clientNotification.AddSweetNotification("Success","Your pet was added!",NotificationType.success);

            } catch (Exception ex){
                _clientNotification.AddSweetNotification("Error","Your pet was not added!",NotificationType.error);
                
            }
            return RedirectToAction("MyPets","Home");
        }

        [TypeFilter(typeof(UserLogAttribute),Arguments = new Object[] {"Delete Action of Pet controller"})]
        public IActionResult Delete(int Id){
            try{
                _petRepository.Delete(_petRepository.GetPet(Id));
                _clientNotification.AddSweetNotification("Success","Pet Deleted",NotificationType.success);
                return Ok();
            } catch (Exception ex){
                _clientNotification.AddSweetNotification("Could not delete pet. Please try again!","",NotificationType.error);                
                return BadRequest();
            }
        }

        public IActionResult UploadImage(IFormFile file){
            if(file==null || file.Length==0)
                return Content("File not selected");
            if(!file.ContentType.Equals("image/png")){
                return Content("Invalid file type. Try again!");
            }

            TempData["fileName"] = new FileUploader().Upload(file);
            return Ok("Success");
        }

        public IActionResult ToggleToSellingList(int Id){
            Pet pet = _petRepository.GetPet(Id);

            if(!pet.OwnerId.Equals(_userManager.GetUserId(HttpContext.User)))
                return Content("You are not authorized for this action");

            pet.IsSelling = !pet.IsSelling;
            _petRepository.Update(pet);

            //Send notification to users who has kept this pet on their watchlists
            if(pet.IsSelling){
                var createNotification = new CreateNotifications(_watchlistRepository,
                                                    _notificationRepository,
                                                    _hubContext);
                // Hangfire
                //     .BackgroundJob
                //     .Enqueue(()=>createNotification
                //     .CreateNotification(Id));
            }

            return RedirectToAction(nameof(Detail),new{Id=Id});
        }

        public IActionResult BuyPet(int Id){
            ViewBag.Id=Id;

            var userId = _userManager.GetUserId(HttpContext.User);

            var pet = new Pet().LockPetForBuying(_petRepository.GetPet(Id),userId);
            
            _petRepository.Update(pet);

            //Schedule to cancel buy after 3 hours if buying does not succeed
            
            UpdatePet update = new UpdatePet(_updatePetRepository);
            string jobId = update.Update(pet);
            TempData["jobId"]=jobId;

            return View();
        }

        public async Task<IActionResult> ConfirmBuying(int Id){
            var pet = _petRepository.GetPet(Id);
    
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if(user!=null){
                var email = user.Email;
                var userId = user.Id;
                
                Hangfire
                .BackgroundJob
                .Enqueue(()=>MailSender.SendMail(pet.Owner.Email,email,Id,userId)); 
            }

            return Content("Request email was successfully sent to owner!");
        }

        public IActionResult ConfirmSelling(int Id, string userId){
            var pet = new Pet().ChangeOwnership(_petRepository.GetPet(Id),userId);
            
            UpdatePet update = new UpdatePet(_updatePetRepository);

            if(TempData["jobId"]!=null)
                update.CancleJob(TempData["jobId"].ToString());
            
            TempData.Clear();

            _petRepository.UpdateWithHistory(pet);
            
            return RedirectToAction(nameof(Index));
        }
    }
}