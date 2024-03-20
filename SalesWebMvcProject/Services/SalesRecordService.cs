using Microsoft.EntityFrameworkCore;
using SalesWebMvcProject.Data;
using SalesWebMvcProject.Models;

namespace SalesWebMvcProject.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcProjectContext _context;

        public SalesRecordService(SalesWebMvcProjectContext context)
        {
            _context = context;
        }

        // get data SellerRecords 
        public async Task<List<SalesRecord>> FindAllSalesRecordServiceAsync()
        {
            return await _context.SalesRecord.ToListAsync();
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;

            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value); 
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value); 
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync(); 
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department)
                .ToListAsync();
        }
    }
}
