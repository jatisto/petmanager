using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace pet_manager.Models
{
    public class Pet
    {
        public Pet()
        {
            Histories = new List<History>();
            Watchlists = new List<Watchlist>();
        }
        public int Id { get; set; }

        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }
        
        [Display(Name="Age")]
        public int Age { get; set; }
        
        [Display(Name="Color")]
        public string Color { get; set; }
        
        [Display(Name="Location")]
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        
        [Display(Name="Description")]
        public string Description { get; set; }
        public bool IsSelling { get; set; } = false;
        public bool IsLocked { get; set; } = false;
        public string LockedByUserId { get; set; }
        
        [JsonIgnore]
        public Owner Owner { get; set; }
        public string OwnerId { get; set; }
        public ICollection<Watchlist> Watchlists { get; set; }
        public ICollection<History> Histories { get; set; }

        public Pet ChangeOwnership(Pet pet, string userId){
            if(pet==null)
                return null;
            
            pet.OwnerId = userId;
            pet.IsSelling = false;
            
            pet.IsLocked = false;
            pet.LockedByUserId = "";
            
            return pet;
        }

        public Pet LockPetForBuying(Pet pet, string userId){
            pet.IsLocked = true;
            pet.LockedByUserId = userId;
            
            return pet;
        }
    }
}