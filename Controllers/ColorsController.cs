using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab5.Models;

namespace Lab5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly MakeUpContext _context;

        public ColorsController(MakeUpContext context)
        {
            _context = context;
        }

        // GET: api/Colors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<_Color>>> GetColors()
        {
            return await _context.Colors.ToListAsync();
        }

        // GET: api/Colors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<_Color>> Get_Color(int id)
        {
            var _Color = await _context.Colors.FindAsync(id);

            if (_Color == null)
            {
                return NotFound();
            }

            return _Color;
        }

        // PUT: api/Colors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Put_Color(int id, _Color _Color)
        {
            if (id != _Color.Id)
            {
                return BadRequest();
            }

            _context.Entry(_Color).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_ColorExists(id))
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

        // POST: api/Colors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<_Color>> Post_Color(_Color _Color)
        {
            _context.Colors.Add(_Color);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get_Color", new { id = _Color.Id }, _Color);
        }

        // DELETE: api/Colors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<_Color>> Delete_Color(int id)
        {
            var _Color = await _context.Colors.FindAsync(id);
            if (_Color == null)
            {
                return NotFound();
            }

            _context.Colors.Remove(_Color);
            await _context.SaveChangesAsync();

            return _Color;
        }

        private bool _ColorExists(int id)
        {
            return _context.Colors.Any(e => e.Id == id);
        }
    }
}
