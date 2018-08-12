using System.Collections.Generic;
using pet_manager.Models;

namespace pet_manager.Repository
{
    public interface IPetRepository
    {
        void Save(Pet pet);
        void Update(Pet pet);
        void Delete(Pet pet);
        List<Pet> GetAllPets();
        Pet GetPet(int Id);
        List<Pet> GetUserPets(string userId);
        List<History> GetPetWithHistories(int Id);
        List<Pet> GetSellingPets();
        void UpdateWithHistory(Pet pet);
    }
}