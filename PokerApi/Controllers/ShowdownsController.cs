using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokerApi.Models;

namespace PokerApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ShowdownsController : Controller
    {
        private readonly ShowdownContext _context;

        public ShowdownsController(ShowdownContext context)
        {
            _context = context;
        }

        // GET: api/Showdowns
        [HttpGet]
        public IEnumerable<Showdown> GetShowdowns()
        {
            return _context.Showdowns;
        }

        // GET: api/Showdowns/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShowdown([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var showdown = await _context.Showdowns.SingleOrDefaultAsync(m => m.Id == id);

            if (showdown == null)
            {
                return NotFound();
            }

            return Ok(showdown);
        }

        // PUT: api/Showdowns/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShowdown([FromRoute] long id, [FromBody] Showdown showdown)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != showdown.Id)
            {
                return BadRequest();
            }

            _context.Entry(showdown).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShowdownExists(id))
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

        // POST: api/Showdowns
        [HttpPost]
        public async Task<IActionResult> PostShowdown([FromBody] Showdown showdown)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Showdowns.Add(showdown);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShowdown", new { id = showdown.Id }, showdown);
        }

        // DELETE: api/Showdowns/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShowdown([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var showdown = await _context.Showdowns.SingleOrDefaultAsync(m => m.Id == id);
            if (showdown == null)
            {
                return NotFound();
            }

            _context.Showdowns.Remove(showdown);
            await _context.SaveChangesAsync();

            return Ok(showdown);
        }

        private bool ShowdownExists(long id)
        {
            return _context.Showdowns.Any(e => e.Id == id);
        }
    }
}