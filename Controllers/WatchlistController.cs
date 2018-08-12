using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pet_manager.Models;
using pet_manager.Repository;

namespace pet_manager.Controllers{
    
    [Authorize]
    public class WatchlistController:Controller
    {
        private IWatchlistRepository _repository;
        private UserManager<Owner> _userManager;
        private INotificationRepository _notificationRepository;

        public WatchlistController(IWatchlistRepository repository, 
                                    UserManager<Owner> userManager,
                                    INotificationRepository notificationRepository)
        {
            _repository = repository;
            _userManager = userManager;
            _notificationRepository=notificationRepository;
        }

        public IActionResult Index(){
            string UserId = _userManager.GetUserId(HttpContext.User);

            try{
                return View(_repository.GetWatchlists(UserId));
            } catch (Exception ex){
                throw ex;
            }
        }

        public IActionResult New(int petId){
            try{
                string UserId = _userManager.GetUserId(HttpContext.User);

                Watchlist watchlist = new Watchlist();
                watchlist.PetId = petId;

                watchlist.UserId = UserId;

                if(!_repository.IsWatchlistExists(petId,UserId)){
                    _repository.Save(watchlist);
                } else {
                    return Content("Already exists in your watchlists");
                }
                return RedirectToAction(nameof(Index));
            } catch(Exception ex){
                throw ex;
            }
        }

        public IActionResult Remove(int Id){
            if(Id==0){
                return Content("Invalid request");
            }

            try{
                _repository.Remove(_repository.GetWatchlist(Id));
                return RedirectToAction(nameof(Index));
            } catch(Exception ex){
                throw ex;
            }
        }
    }
}