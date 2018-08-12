using pet_manager.Data;
using pet_manager.Models;

namespace pet_manager.BackgroundJob
{
    public class UpdatePetRepository : IUpdatePetRepository
    {
        private ApplicationDbContext _context;

        public UpdatePetRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Update(Pet pet)
        {
            pet.IsLocked = false;
            pet.LockedByUserId = "";

            _context.Pets.Update(pet);
            _context.SaveChanges();
        }
    }
}