﻿using System;
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
    public class ProductColorsController : ControllerBase
    {
        private readonly MakeUpContext _context;

        public ProductColorsController(MakeUpContext context)
        {
            _context = context;
        }

        // GET: api/ProductColors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductColor>>> GetProductColors()
        {
            return await _context.ProductColors.ToListAsync();
        }

        // GET: api/ProductColors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductColor>> GetProductColor(int id)
        {
            var productColor = await _context.ProductColors.FindAsync(id);

            if (productColor == null)
            {
                return NotFound();
            }

            return productColor;
        }

        // PUT: api/ProductColors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductColor(int id, ProductColor productColor)
        {
            if (id != productColor.Id)
            {
                return BadRequest();
            }

            ProductColorValid valid = new ProductColorValid(_context, productColor);
            if (valid.Valid() == false) return BadRequest("Данное косметическое средство в этом цвете уже существует");

            _context.Entry(productColor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductColorExists(id))
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

        // POST: api/ProductColors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProductColor>> PostProductColor(ProductColor productColor)
        {
            ProductColorValid valid = new ProductColorValid(_context, productColor);
            if (valid.Valid() == false) return BadRequest("Данное косметическое средство в этом цвете уже существует");

            _context.ProductColors.Add(productColor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductColor", new { id = productColor.Id }, productColor);
        }

        // DELETE: api/ProductColors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductColor>> DeleteProductColor(int id)
        {
            var productColor = await _context.ProductColors.FindAsync(id);
            if (productColor == null)
            {
                return NotFound();
            }

            _context.ProductColors.Remove(productColor);
            await _context.SaveChangesAsync();

            return productColor;
        }

        private bool ProductColorExists(int id)
        {
            return _context.ProductColors.Any(e => e.Id == id);
        }
    }
}
