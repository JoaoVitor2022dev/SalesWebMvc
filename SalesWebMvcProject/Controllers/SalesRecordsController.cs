using Microsoft.AspNetCore.Mvc;
using SalesWebMvcProject.Models;
using SalesWebMvcProject.Models.ViewModels;
using SalesWebMvcProject.Services;

namespace SalesWebMvcProject.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;
        private readonly SellerService _sellerService;

        public SalesRecordsController(SalesRecordService salesRecordService, SellerService sellerService)
        {
            _salesRecordService = salesRecordService;
            _sellerService = sellerService;
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

        // page criate
        public async Task<IActionResult> Create()
        {
            var Sellers = await _sellerService.FindAllSellerAsync();

            var viewModel = new SalesRecordsFormViewModel { Sellers = Sellers };
            return View(viewModel);
        }

        // inserir vendas no sistema 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalesRecord salesRecord)
        {
            await _salesRecordService.InsertAsync(salesRecord);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);  
            }
            if (!maxDate.HasValue) 
            {
                maxDate = DateTime.Now;                                      
            }

            // enviar dados por meio de view data
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

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
