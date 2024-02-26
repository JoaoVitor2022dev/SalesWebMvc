using Microsoft.AspNetCore.Mvc;
using SalesWebMvcProject.Models;

namespace SalesWebMvcProject.Controllers
{
    public class DepartmentsController : Controller
    {
        public IActionResult Index()
        {

             List<Department> list = new List<Department>();
             list.Add(new Department { Id = 1, Name = "Eletronics" });
             list.Add(new Department { Id = 1, Name = "tvs" }); 
             list.Add(new Department { Id = 1, Name = "Fashion" });
             list.Add(new Department { Id = 1, Name = "Collegio" });

            return View(list);
        }
    }
}
