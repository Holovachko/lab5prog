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
    public class cardsController : ControllerBase
    {
        private readonly DREPLAB5LAB5MDFContext _context;

        public cardsController(DREPLAB5LAB5MDFContext context)
        {
            _context = context;
        }

        // GET: api/cards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cards>>> Getcards(int? minpages, string title)
        {
            var cards = _context.cards.Where(card => true);
            if (minpages.HasValue)
            {
               cards = cards.Where(card => card.Pages >= minpages.Value);
            }

            if (title != null)
            {
               cards = cards.Where(card => card.Title.ToLower().Contains(title.ToLower()));
            }

            return await cards.Include(card => card.Color).ToListAsync();
        }

        // GET: api/cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<cards>> Getcards(int id)
        {
            var cards = await _context.cards.FindAsync(id);

            if (cards == null)
            {
                return NotFound();
            }

            return cards;
        }

        // PUT: api/cards/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Putcards(int id, cards cards)
        {
            if (id != cards.Id)
            {
                return BadRequest();
            }

            _context.Entry(cards).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cardsExists(id))
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

        // POST: api/cards
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<cards>> Postcards(cards cards)
        {
            _context.cards.Add(cards);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getcards", new { id = cards.Id }, cards);
        }

        // DELETE: api/cards/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<cards>> Deletecards(int id)
        {
            var cards = await _context.cards.FindAsync(id);
            if (cards == null)
            {
                return NotFound();
            }

            _context.cards.Remove(cards);
            await _context.SaveChangesAsync();

            return cards;
        }

        private bool cardsExists(int id)
        {
            return _context.cards.Any(e => e.Id == id);
        }
    }
}
