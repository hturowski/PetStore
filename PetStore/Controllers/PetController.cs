using Microsoft.AspNetCore.Mvc;
using PetStore.DAO;
using System.Linq;
using System.Collections;

namespace PetStore
{
    [Route("pet")]
    public class PetController : Controller
    {
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using (var db = new PetStoreContext())
            {
                try {
                    var animal = db.Pets
                        .Single(b => b.Id == id);
                    return Ok(animal);
                }
                catch(System.Exception e)
                {
                    return NotFound(e);
                }
            }
        }

        [HttpPut()]
        public IActionResult Put(int id)
        {
            using (var db = new PetStoreContext())
            {
                db.Pets.Add(new Pet(2, "Zoey", "Cat"));
                db.SaveChanges();
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody]Pet newPet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var db = new PetStoreContext())
            {
                db.Pets.Add(newPet);
                db.SaveChanges();
            }
            return Ok(newPet);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var db = new PetStoreContext())
            {
                try
                {
                    var toRemove = db.Pets.Single(p => p.Id == id);
                    db.Pets.Remove(toRemove);
                    db.SaveChanges();
                }
                catch(System.Exception e )
                {
                    return NotFound(e);
                }
            }
            return Ok();
        }
    }
}
