﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab5.Models;
using Microsoft.AspNetCore.Http.Connections;
using System.Text.Json.Serialization;

namespace Lab5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly MakeUpContext _context;

        public AreasController(MakeUpContext context)
        {
            _context = context;
        }

        // GET: api/Areas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Area>>> GetAreas()
        {
            return await _context.Areas.ToListAsync();
        }

        // GET: api/Areas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Area>> GetArea(int id)
        {
            var area = await _context.Areas.FindAsync(id);

            if (area == null)
            {
                return NotFound();
            }

            return area;
        }

        // PUT: api/Areas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArea(int id, Area area)
        {
            if (id != area.Id)
            {
                return BadRequest();
            }

            AreaValid valid = new AreaValid(_context, area);
            if (valid.Valid() == false) return BadRequest("Данная область приминения уже существует");

            _context.Entry(area).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaExists(id))
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

        // POST: api/Areas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Area>> PostArea(Area area)
        {
            AreaValid valid = new AreaValid(_context, area);
            if (valid.Valid() == false) return BadRequest("Данная область приминения уже существует");

            _context.Areas.Add(area);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArea", new { id = area.Id }, area);
        }

        // DELETE: api/Areas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Area>> DeleteArea(int id)
        {
            var area = await _context.Areas.FindAsync(id);
            if (area == null)
            {
                return NotFound();
            }

            var cosmetics = _context.Cosmetics.Where(c => c.Id_Area == id).ToList();
            if(cosmetics.Count != 0)
            {
                foreach(var cosmetic in cosmetics)
                {
                    var products = _context.Products.Where(p => p.Id_Cosmetics == cosmetic.Id).ToList();
                    if (products.Count != 0)
                    {
                        foreach(var product in products)
                        {
                            var productcolor = _context.ProductColors.Where(p => p.Id_Product == product.Id).ToList();
                            if (productcolor.Count != 0)
                            {
                                foreach (var productt in productcolor)
                                {
                                    var prc = await _context.Products.FindAsync(productt.Id);
                                    _context.Remove(prc);
                                    await _context.SaveChangesAsync();
                                }
                            }
                            var pr = await _context.Products.FindAsync(product.Id);
                            _context.Remove(pr);
                            await _context.SaveChangesAsync();
                        }
                    }
                    var cs = await _context.Cosmetics.FindAsync(cosmetic.Id);
                    _context.Remove(cs);
                    await _context.SaveChangesAsync();
                }
            }

            _context.Areas.Remove(area);
            await _context.SaveChangesAsync();

            return area;
        }

        private bool AreaExists(int id)
        {
            return _context.Areas.Any(e => e.Id == id);
        }
    }
}
