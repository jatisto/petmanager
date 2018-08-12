using pet_manager.Models;

namespace pet_manager.BackgroundJob
{
    public interface IUpdatePetRepository
    {
        void Update(Pet pet);   
    }
}