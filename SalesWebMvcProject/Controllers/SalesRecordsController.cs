using Microsoft.AspNetCore.Mvc;
using SalesWebMvcProject.Services;

namespace SalesWebMvcProject.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }
        // funcionalidade de dados
        public async Task<IActionResult> Details()
        {
            var list = await _salesRecordService.FindAllSalesRecordServiceAsync();
            return View(list);
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            // Define minDate como o primeiro dia do ano atual se não estiver definido
            minDate ??= new DateTime(DateTime.Now.Year, 1, 1);

            // Define maxDate como a data atual se não estiver definido
            maxDate ??= DateTime.Now;

            // Garante que minDate não seja maior que maxDate
            if (minDate > maxDate)
            {
                ModelState.AddModelError("", "A data mínima não pode ser maior que a data máxima.");
                return View();
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            // Chama o método de serviço para buscar os registros
            var result = await _salesRecordService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }
        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _salesRecordService.FindByDateGroupingAsync(minDate, maxDate);
            return View(result);
        }
    }
}
