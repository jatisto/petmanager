using System;
using pet_manager.Models;

namespace pet_manager.BackgroundJob
{
    public class UpdatePet
    {
        private IUpdatePetRepository _updatePetRepository;
        public UpdatePet(IUpdatePetRepository updatePetRepository)
        {
            _updatePetRepository = updatePetRepository;
        }

        public string Update(Pet pet){
            string jobId = Hangfire.BackgroundJob.Schedule(()=> _updatePetRepository.Update(pet),
                                        TimeSpan.FromMinutes(1));
            return jobId;
        }

        public void CancleJob(string jobId){
            Hangfire.BackgroundJob.Delete(jobId);
        }
    }
}