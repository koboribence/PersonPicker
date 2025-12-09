using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonPicker.Data;

namespace PersonPicker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrawsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DrawsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/draws
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DrawDto>>> GetDraws()
        {
            var draws = await _context.Draws
                .Include(d => d.Person)
                .OrderByDescending(d => d.SelectedAt)
                .ToListAsync();

            var result = draws.Select(d => new DrawDto
            {
                Id = d.Id,
                PersonName = d.Person != null ? d.Person.Name : "(ismeretlen)",
                SelectedAt = d.SelectedAt
            }).ToList();

            return Ok(result);
        }
    }
    public class DrawDto
    {
        public string Id { get; set; } = string.Empty;
        public string PersonName { get; set; } = string.Empty;
        public DateTime SelectedAt { get; set; }
    }
}
