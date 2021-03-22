using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarea1DWBE.DataAccess;

namespace Tarea1DWBE.Services
{
    public class ProductsS
    {
        NorthwindContext dataContext = new NorthwindContext();
        public IQueryable<Products> GetAllProducts()
        {
            return dataContext.Products.Select(s => s);
        }

    }
}
