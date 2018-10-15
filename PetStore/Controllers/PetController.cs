using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace PetStore
{
    [Route("pet")]
    public class PetController : Controller
    {
        static Hashtable petDb = new Hashtable();
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 1)
            {
                var result =
                    Ok(new Pet(1, "Feynman", "Dog"));
                return result;
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut()]
        public IActionResult Put()
        {
            petDb.Add("1", new Pet(1, "Feynman", "Dog"));
            return Ok();
        }
    }
}
