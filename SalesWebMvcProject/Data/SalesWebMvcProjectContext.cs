using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvcProject.Models;

namespace SalesWebMvcProject.Data
{
    public class SalesWebMvcProjectContext : DbContext
    {
        public SalesWebMvcProjectContext (DbContextOptions<SalesWebMvcProjectContext> options)
            : base(options)
        {
        }

        public DbSet<SalesWebMvcProject.Models.Department> Department { get; set; } = default!;
    }
}
