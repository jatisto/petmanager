using Microsoft.AspNetCore.SignalR;
using pet_manager.Infrastructure;
using pet_manager.Repository;

namespace pet_manager.BackgroundJob
{
    public class CreateNotifications
    {
        private IWatchlistRepository _watchlistRepository;
        private INotificationRepository _notificationRepository;
        private IHubContext<SignalServer> _hubContext;
        public CreateNotifications(IWatchlistRepository watchlistRepository,
                            INotificationRepository notificationRepository,
                            IHubContext<SignalServer> hubContext)
        {
            _watchlistRepository = watchlistRepository;
            _notificationRepository = notificationRepository;
            _hubContext = hubContext;
        }
        public void CreateNotification(int petId){

            var watchlists = _watchlistRepository.GetWatchlistFromPetId(petId);
                foreach (var watchlist in watchlists)
                {
                    _notificationRepository.CreateNotification(petId,watchlist.UserId);
                }
            _hubContext.Clients.All.InvokeAsync("readNotifications","");
        }
    }
}