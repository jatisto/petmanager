using System.Collections.Generic;
using pet_manager.Models;

namespace pet_manager.Repository{
    public interface IWatchlistRepository
    {
        void Save(Watchlist watchlist);
        void Remove(Watchlist watchlist);
        List<Watchlist> GetWatchlists(string userId);
        Watchlist GetWatchlist(int Id);
        bool IsWatchlistExists(int petId, string userId);
        List<Watchlist> GetWatchlistFromPetId(int petId);
    }
}