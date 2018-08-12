using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace pet_manager.Models
{
    public class Owner:IdentityUser
    {
        public Owner()
        {
            Pets = new List<Pet>();
            Watchlists = new List<Watchlist>();
            Histories = new List<History>();
            Notifications = new List<Notification>();
        }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public ICollection<Pet> Pets { get; set; }
        public ICollection<Watchlist> Watchlists { get; set; }
        public ICollection<History> Histories { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}