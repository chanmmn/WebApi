using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private static List<Person> Persons = new List<Person>
        {
            new Person { Id = 1, FullName = "John Doe" },
            new Person { Id = 2, FullName = "Jane Smith" }
        };

        [HttpGet(Name = "GetPerson")]
        public IEnumerable<Person> Get()
        {
            //Simulate an exception Error 500
            throw new Exception();
            return Persons;
        }

        [HttpGet("{id}")]
        public ActionResult<Person> Get(int id)
        {
            var person = Persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return person;
        }

        [HttpPost]
        public ActionResult<Person> Post([FromBody] Person person)
        {
            person.Id = Persons.Max(p => p.Id) + 1;
            Persons.Add(person);
            return CreatedAtAction(nameof(Get), new { id = person.Id }, person);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Person person)
        {
            var existingPerson = Persons.FirstOrDefault(p => p.Id == id);
            if (existingPerson == null)
            {
                return NotFound();
            }
            existingPerson.FullName = person.FullName;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var person = Persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            Persons.Remove(person);
            return NoContent();
        }
    }
}
