using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarea1DWBE.DataAccess;
using Tarea1DWBE.Services;

namespace Tarea1DWBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static readonly ProductsS request = new ProductsS();

        // GET: api/Products
        [HttpGet]
        public IEnumerable<Products> GetProducts()
        {
            return request.GetAllProducts();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProducts(int id)
        {
            var context = new NorthwindContext();

            var products = context.Products.Find(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts(int id, Products products)
        {
            var context = new NorthwindContext();

            if (id != products.ProductId)
            {
                return BadRequest();
            }

            context.Entry(products).State = EntityState.Modified;

            try
            {
                context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Products>> PostProducts(Products products)
        {
            var context = new NorthwindContext();

            context.Products.Add(products);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetProducts", new { id = products.ProductId }, products);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Products>> DeleteProducts(int id)
        {
            var context = new NorthwindContext();

            var products = context.Products.Find(id);
            if (products == null)
            {
                return NotFound();
            }

            context.Products.Remove(products);
            context.SaveChangesAsync();

            return products;
        }

        private bool ProductsExists(int id)
        {
            var context = new NorthwindContext();

            return context.Products.Any(e => e.ProductId == id);
        }
    }
}
