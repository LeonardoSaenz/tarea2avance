using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarea1DWBE.DataAccess;

namespace Tarea1DWBE.Services
{
    public class SuppliersS
    {
        NorthwindContext dataContext = new NorthwindContext();
        public IQueryable<Suppliers> GetAllSuppliers()
        {
            return dataContext.Suppliers.Select(s => s);
        }
    }
}
