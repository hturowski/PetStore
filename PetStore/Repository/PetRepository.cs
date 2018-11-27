using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PetStore.DAO;

namespace PetStore.Repository
{
    public class PetRepository : IPetRepository {
        public IEnumerable<Pet> GetAllPets() {
            using (var db = new PetStoreContext()) {
                return db.Pets.ToList();
            }
        }

        public Pet GetPet(int petId) {
            using (var db = new PetStoreContext()) {
                return db.Pets.FirstOrDefault(p => p.Id == petId);
            }
        }

        public void AddPet(Pet pet) {
            using (var db = new PetStoreContext()) {
                db.Pets.Add(pet);
                db.SaveChanges();
            }
        }

        public bool DeletePet(int petId) {
            using (var db = new PetStoreContext()) {
                var toRemove = db.Pets.FirstOrDefault(p => p.Id == petId);
                if (toRemove != null) {
                    db.Pets.Remove(toRemove);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
