using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using pet_manager.Data;
using pet_manager.Models;

namespace pet_manager.Repository
{
    public class WatchlistRepository:IWatchlistRepository
    {
        private ApplicationDbContext _context;
        public WatchlistRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Watchlist> GetWatchlists(string userId)
        {
            return _context.Watchlists
                            .Where(watchlist=>watchlist.UserId.Equals(userId))
                            .Include(w=>w.Pet)
                            .Include(w=>w.Pet.Owner)
                            .ToList();
        }

        public List<Watchlist> GetWatchlistFromPetId(int petId){
            return _context.Watchlists
                            .Where(w=>w.PetId ==petId)
                            .ToList();
        }

        public void Remove(Watchlist watchlist)
        {
            _context.Watchlists.Remove(watchlist);
            _context.SaveChanges();
        }

        public void Save(Watchlist watchlist)
        {
            _context.Watchlists.Add(watchlist);
            _context.SaveChanges();
        }

        public Watchlist GetWatchlist(int Id){
            return _context.Watchlists.FirstOrDefault(watchlist=>watchlist.Id==Id);
        }

        public bool IsWatchlistExists(int petId, string userId){
            return _context.Watchlists
                            .Any(w=>w.PetId==petId && w.UserId.Equals(userId));
        }
    }
}