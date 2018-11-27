using Microsoft.AspNetCore.Mvc;
using PetStore.DAO;
using PetStore.Repository;

namespace PetStore
{
    [Route("pets")]
    public class PetController : Controller
    {
        private IPetRepository PetRepo { get; set; }

        public PetController(IPetRepository petRepo) {
            PetRepo = petRepo;
        }

        [HttpGet]
        public IActionResult Get() {
            var pets = PetRepo.GetAllPets();
            return Ok(pets);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            var animal = PetRepo.GetPet(id);
            if (animal != null) {
                return Ok(animal);
            }
            return NotFound();
        }

        [HttpPut()]
        public IActionResult Put(int id) {
            using (var db = new PetStoreContext())  {
                db.Pets.Add(new Pet(2, "Amos", "Cat"));
                db.SaveChanges();
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody]Pet newPet) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            PetRepo.AddPet(newPet);
            return Ok(newPet);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            if (PetRepo.DeletePet(id)) {
                return Ok();
            }
            return NotFound();
        }
    }
}
