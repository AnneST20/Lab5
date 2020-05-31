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
    public class CountriesController : ControllerBase
    {
        private readonly MakeUpContext _context;

        public CountriesController(MakeUpContext context)
        {
            _context = context;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Country country)
        {
            if (id != country.Id)
            {
                return BadRequest();
            }

            CountryValid valid = new CountryValid(_context, country);
            if (valid.Valid() == false) return BadRequest("Данная страна уже существует");

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
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

        // POST: api/Countries
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(Country country)
        {
            CountryValid valid = new CountryValid(_context, country);
            if (valid.Valid() == false) return BadRequest("Данная страна уже существует");

            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Country>> DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            var firms = _context.Firms.Where(f => f.Id_Country == id).ToList();
            if(firms.Count != 0)
            {
                foreach(var firm in firms)
                {
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
                    var f = await _context.Firms.FindAsync(firm.Id);
                    _context.Remove(f);
                    await _context.SaveChangesAsync();
                }
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return country;
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}
