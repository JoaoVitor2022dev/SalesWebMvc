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

        public DbSet<Department> Department { get; set; } = default!;
        public DbSet<Seller> Seller { get; set; } = default!;
        public DbSet<SalesRecord> SalesRecord { get; set; } = default!;
    }
}
