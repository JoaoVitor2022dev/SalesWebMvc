using Microsoft.AspNetCore.Mvc;
using SalesWebMvcProject.Data;
using SalesWebMvcProject.Models;

namespace SalesWebMvcProject.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcProjectContext _context;

        public SellerService(SalesWebMvcProjectContext context)
        {
            _context = context;
        }

        // get data in database
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList(); 
        }

        // insert data in database
        internal void Insert(Seller obj)
        {
            _context.Add(obj); 
            _context.SaveChanges();
        }
    }
}
