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

    public class CategoriesServices : CategoriesS
    {
        public static CategoriesS CategoriesService = new CategoriesS();

        public static void Tarea2()
        {
            var output = CategoriesService.GetAllCategories().ToList();
        }
    }
        

    [Route("api/[controller]")] 
    [ApiController]
    public class CategoriesController : ControllerBase
    {       
        // GET: api/Categories
        [HttpGet]
        public IEnumerable<Categories> GetCategories()
        {
            using (var dataContext = new NorthwindContext())
            {
                var categoriesQuery = dataContext.Categories.Select(s => s);
                var output = categoriesQuery.ToList();
                return output;
            }
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categories>> GetCategories(int id)
        {
            var context = new NorthwindContext();

            var categories = await context.Categories.FindAsync(id);

            if (categories == null)
            {
                return NotFound();
            }

            return categories;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategories(int id, Categories categories)
        {
            var context = new NorthwindContext();

            if (id != categories.CategoryId)
            {
                return BadRequest();
            }

            context.Entry(categories).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriesExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Categories>> PostCategories(Categories categories)
        {
            var context = new NorthwindContext();

            context.Categories.Add(categories);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetCategories", new { id = categories.CategoryId }, categories);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categories>> DeleteCategories(int id)
        {
            var context = new NorthwindContext();

            var categories = await context.Categories.FindAsync(id);
            if (categories == null)
            {
                return NotFound();
            }

            context.Categories.Remove(categories);
            await context.SaveChangesAsync();

            return categories;
        }

        private bool CategoriesExists(int id)
        {
            var context = new NorthwindContext();

            return context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
