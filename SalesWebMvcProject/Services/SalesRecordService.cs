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
            // Defina um valor padrão para minDate e maxDate se eles forem nulos
            minDate ??= DateTime.MinValue;
            maxDate ??= DateTime.MaxValue;

            // Verifica se minDate é menor que maxDate, caso ambos sejam fornecidos
            if (minDate > maxDate)
            {
                throw new ArgumentException("minDate não pode ser maior que maxDate.");
            }

            // Consulta inicial
            IQueryable<SalesRecord> result = _context.SalesRecord.AsQueryable();

            // Adiciona as condições de filtro para minDate e maxDate
            result = result.Where(x => x.Date >= minDate && x.Date <= maxDate);

            // Inclui os relacionamentos necessários
            result = result.Include(x => x.Seller)
                           .ThenInclude(x => x.Department);

            // Ordena os registros por data em ordem decrescente
            result = result.OrderByDescending(x => x.Date);

            // Executa a consulta e retorna os resultados como uma lista assíncrona
            return await result.ToListAsync();
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
