using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
