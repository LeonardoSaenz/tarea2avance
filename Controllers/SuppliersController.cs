using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tarea1DWBE.DataAccess;

namespace Tarea1DWBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {   
        // GET: api/Suppliers
        [HttpGet]
        public IEnumerable<Suppliers> GetSuppliers()
        {
            using (var dataContext = new NorthwindContext())
            {
                var suppliersQuery = dataContext.Suppliers.Select(s => s);
                var output = suppliersQuery.ToList();
                return output;
            }
        }
        
        // GET: api/Suppliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Suppliers>> GetSuppliers(int id)
        {
            var context = new NorthwindContext();

            var suppliers = context.Suppliers.FindAsync(id);

            if (suppliers == null)
            {
                return NotFound();
            }

            return await suppliers;
        }

        // PUT: api/Suppliers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSuppliers(int id, Suppliers suppliers)
        {
            var context = new NorthwindContext();

            if (id != suppliers.SupplierId)
            {
                return BadRequest();
            }

            context.Entry(suppliers).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuppliersExists(id))
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

        // POST: api/Suppliers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Suppliers>> PostSuppliers(Suppliers suppliers)
        {
            var context = new NorthwindContext();

            context.Suppliers.Add(suppliers);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetSuppliers", new { id = suppliers.SupplierId }, suppliers);
        }

        // DELETE: api/Suppliers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Suppliers>> DeleteSuppliers(int id)
        {
            var context = new NorthwindContext();

            var suppliers = await context.Suppliers.FindAsync(id);
            if (suppliers == null)
            {
                return NotFound();
            }

            context.Suppliers.Remove(suppliers);
            await context.SaveChangesAsync();

            return suppliers;
        }

        private bool SuppliersExists(int id)
        {
            var context = new NorthwindContext();

            return context.Suppliers.Any(e => e.SupplierId == id);
        }
    }
}
