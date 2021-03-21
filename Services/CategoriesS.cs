using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarea1DWBE.DataAccess;

namespace Tarea1DWBE.Services
{
    public class CategoriesS
    {
        NorthwindContext dataContext = new NorthwindContext();
        public IQueryable<Categories> GetAllCategories()
        {
            return dataContext.Categories.Select(s => s);
        }
    }



}
