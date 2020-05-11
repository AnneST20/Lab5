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
    public class CosmeticsController : ControllerBase
    {
        private readonly MakeUpContext _context;

        public CosmeticsController(MakeUpContext context)
        {
            _context = context;
        }

        // GET: api/Cosmetics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cosmetic>>> GetCosmetics()
        {
            return await _context.Cosmetics.ToListAsync();
        }

        // GET: api/Cosmetics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cosmetic>> GetCosmetic(int id)
        {
            var cosmetic = await _context.Cosmetics.FindAsync(id);

            if (cosmetic == null)
            {
                return NotFound();
            }

            return cosmetic;
        }

        // PUT: api/Cosmetics/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCosmetic(int id, Cosmetic cosmetic)
        {
            if (id != cosmetic.Id)
            {
                return BadRequest();
            }

            _context.Entry(cosmetic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CosmeticExists(id))
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

        // POST: api/Cosmetics
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Cosmetic>> PostCosmetic(Cosmetic cosmetic)
        {
            _context.Cosmetics.Add(cosmetic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCosmetic", new { id = cosmetic.Id }, cosmetic);
        }

        // DELETE: api/Cosmetics/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cosmetic>> DeleteCosmetic(int id)
        {
            var cosmetic = await _context.Cosmetics.FindAsync(id);
            if (cosmetic == null)
            {
                return NotFound();
            }

            var products = _context.Products.Where(p => p.Id_Cosmetics == id).ToList();
            if (products.Count != 0)
            {
                foreach (var product in products)
                {
                    var productcolors = _context.ProductColors.Where(p => p.Id_Product == product.Id).ToList();
                    if (productcolors.Count != 0)
                    {
                        foreach (var productcolor in productcolors)
                        {
                            var pr = await _context.ProductColors.FindAsync(productcolor.Id);
                            _context.Remove(pr);
                            await _context.SaveChangesAsync();
                        }
                    }
                    var p = await _context.ProductColors.FindAsync(product.Id);
                    _context.Remove(p);
                    await _context.SaveChangesAsync();
                }
            }

            _context.Cosmetics.Remove(cosmetic);
            await _context.SaveChangesAsync();

            return cosmetic;
        }

        private bool CosmeticExists(int id)
        {
            return _context.Cosmetics.Any(e => e.Id == id);
        }
    }
}
