using System.Collections.Generic;

namespace PetStore.Repository
{
    public interface IPetRepository {
        IEnumerable<Pet> GetAllPets();
        Pet GetPet(int petId);
        void AddPet(Pet pet);
        bool DeletePet(int petId);
    }
}
