using Microsoft.AspNetCore.Mvc;
using SalesWebMvcProject.Data;
using SalesWebMvcProject.Models;
using Microsoft.EntityFrameworkCore;
using Humanizer;
using SalesWebMvcProject.Services.Exeptions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SalesWebMvcProject.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcProjectContext _context;

        public SellerService(SalesWebMvcProjectContext context)
        {
            _context = context;
        }

        // get all data from database
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task<List<Seller>> FindAllSellerAsync()
        {
            return await _context.Seller.OrderBy(x => x.Name).ToListAsync();
        }


        // insert data into database
        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        // get data by id 
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        // Delete data from database 
        public async Task RemoveAsync(int id)
        {
            try
            {
               var obj = await _context.Seller.FindAsync(id);
               _context.Seller.Remove(obj);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e) 
            {
                throw new IntegrityExeption(e.Message); 
            }
        }

        // Update data in database
        public async Task UpdateAsync(Seller obj)
        {
            if (!await _context.Seller.AnyAsync(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException("Cant´t delete seller because he/she has sales");
            }
        }
    }
}
