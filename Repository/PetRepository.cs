using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using pet_manager.Data;
using pet_manager.Models;

namespace pet_manager.Repository
{
    public class PetRepository : IPetRepository
    {
        private ApplicationDbContext _context;
        public PetRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Delete(Pet pet)
        {
            _context.Pets.Remove(pet);
            _context.SaveChanges();
        }

        public List<Pet> GetAllPets()
        {
            return _context.Pets.ToList();
        }

        public List<Pet> GetUserPets(string userId){
            return _context.Pets.Where(p=>p.OwnerId.Equals(userId)).ToList();
        }
        public Pet GetPet(int Id)
        {
            return _context.Pets
            .Include(p=>p.Owner)
            .FirstOrDefault(p=>p.Id==Id);
        }

        public List<Pet> GetSellingPets(){
            return _context.Pets.Where(p=>p.IsSelling==true).ToList();
        }

        public List<History> GetPetWithHistories(int Id){
            return _context.Histories
                            .Where(h=>h.PetId==Id)
                            .Include(h=>h.Owner)
                            .Include(h=>h.Pet)
                            .ToList();
        }

        public void Save(Pet pet)
        {
            _context.Pets.Add(pet);
            _context.Histories.Add(new History{PetId = pet.Id, 
                                                OwnerId=pet.OwnerId,
                                                OwnDate=DateTime.Now});
            _context.SaveChanges();
        }

        public void Update(Pet pet)
        {
            _context.Pets.Update(pet);
            _context.SaveChanges();
        }

        public void UpdateWithHistory(Pet pet)
        {
            _context.Pets.Update(pet);
            _context.Histories.Add(new History{PetId = pet.Id, 
                                                OwnerId=pet.OwnerId,
                                                OwnDate=DateTime.Now});
            _context.SaveChanges();
        }
    }
}