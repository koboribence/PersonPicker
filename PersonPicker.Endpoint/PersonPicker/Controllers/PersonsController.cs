using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonPicker.Data;
using PersonPicker.Models;

namespace PersonPicker.Endpoint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly Random _random = new();

        public PersonsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/persons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            var persons = await _context.Persons
                .OrderBy(p => p.Id)
                .ToListAsync();

            return Ok(persons);
        }

        // GET: api/persons/random
        [HttpGet("random")]
        public async Task<ActionResult<Person>> GetRandomPerson()
        {
            var count = await _context.Persons.CountAsync();
            if (count == 0)
            {
                return NotFound("Nincs név az adatbázisban!");
            }

            var index = _random.Next(0, count);

            var person = await _context.Persons
                .OrderBy(p => p.Id)
                .Skip(index)
                .FirstOrDefaultAsync();

            var draw = new Draw
            {
                PersonId = person.Id,
                SelectedAt = DateTime.UtcNow
            };

            _context.Draws.Add(draw);
            await _context.SaveChangesAsync();

            return Ok(person);
        }

        // POST: api/persons
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            if (string.IsNullOrWhiteSpace(person.Name))
            {
                return BadRequest("A név nem lehet üres.");
            }

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersons), new { id = person.Id }, person);
        }

        // DELETE: api/persons/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(string id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}