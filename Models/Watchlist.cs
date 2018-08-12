namespace pet_manager.Models
{
    public class Watchlist{
        public int Id { get; set; }
        public int PetId { get; set; }
        public Owner User{ get; set; }  
        public Pet Pet { get; set; }
        public string UserId { get; set; }      
    }
}