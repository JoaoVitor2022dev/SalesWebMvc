using SalesWebMvcProject.Data;
using SalesWebMvcProject.Models;
using System.Linq; 

namespace SalesWebMvcProject.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcProjectContext _context;

        public DepartmentService(SalesWebMvcProjectContext context)
        {
            _context = context;
        }

        public List<Department> FindAll() 
        {
            return _context.Department.OrderBy(x => x.Name ).ToList();
        }
    }
}
