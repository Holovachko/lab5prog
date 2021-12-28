using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab5.Models;

namespace lab5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly DREPLAB5LAB5MDFContext _context;

        public ColorController(DREPLAB5LAB5MDFContext context)
        {
            _context = context;
        }

        // GET: api/Color
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Color>>> GetColor()
        {
            return await _context.Color.ToListAsync();
        }

        // GET: api/Color/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Color>> GetColor(int id)
        {
            var Color = await _context.Color.FindAsync(id);

            if (Color == null)
            {
                return NotFound();
            }

            return Color;
        }

        // PUT: api/Color/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColor(int id, Color Color)
        {
            if (id != Color.Id)
            {
                return BadRequest();
            }

            _context.Entry(Color).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Color
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Color>> PostColor(Color Color)
        {
            _context.Color.Add(Color);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColor", new { id = Color.Id }, Color);
        }

        // DELETE: api/Color/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Color>> DeleteColor(int id)
        {
            var Color = await _context.Color.FindAsync(id);
            if (Color == null)
            {
                return NotFound();
            }

            _context.Color.Remove(Color);
            await _context.SaveChangesAsync();

            return Color;
        }

        private bool ColorExists(int id)
        {
            return _context.Color.Any(e => e.Id == id);
        }
    }
}
