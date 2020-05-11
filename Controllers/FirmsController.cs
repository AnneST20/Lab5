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
    public class FirmsController : ControllerBase
    {
        private readonly MakeUpContext _context;

        public FirmsController(MakeUpContext context)
        {
            _context = context;
        }

        // GET: api/Firms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Firm>>> GetFirms()
        {
            return await _context.Firms.ToListAsync();
        }

        // GET: api/Firms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Firm>> GetFirm(int id)
        {
            var firm = await _context.Firms.FindAsync(id);

            if (firm == null)
            {
                return NotFound();
            }

            return firm;
        }

        // PUT: api/Firms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFirm(int id, Firm firm)
        {
            if (id != firm.Id)
            {
                return BadRequest();
            }

            _context.Entry(firm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FirmExists(id))
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

        // POST: api/Firms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Firm>> PostFirm(Firm firm)
        {
            _context.Firms.Add(firm);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFirm", new { id = firm.Id }, firm);
        }

        // DELETE: api/Firms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Firm>> DeleteFirm(int id)
        {
            var firm = await _context.Firms.FindAsync(id);
            if (firm == null)
            {
                return NotFound();
            }

            var products = _context.Products.Where(p => p.Id_Firm == id).ToList();
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

            _context.Firms.Remove(firm);
            await _context.SaveChangesAsync();

            return firm;
        }

        private bool FirmExists(int id)
        {
            return _context.Firms.Any(e => e.Id == id);
        }
    }
}
