using System;

namespace pet_manager.Models
{
    public class History{
        public int Id { get; set; }
        public Pet Pet { get; set; }
        public Owner Owner { get; set; }
        public int PetId { get; set; }
        public string OwnerId { get; set; }
        public DateTime OwnDate { get; set; }
    }
}