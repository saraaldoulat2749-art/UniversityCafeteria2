using Microsoft.AspNetCore.Mvc;

namespace UniversityCafeteria.Controllers
{
    public class SupplyController : Controller
    {
        public IActionResult Index()
        {
            string[] Categories = { "Drinks", "Food", "Snacks", "Hot Drinks", "Cold Drinks" };
            ViewBag.Categories = Categories;
            return View();
        }

        public IActionResult Products()
        {
            ViewData["Page Title: "] = "Cafeteria Products";
            ViewData["ManagerName: "] = "Ahmad Ali";
            ViewData["Number of Products: "] = "5";
            string[] products = { "Water", "Orange Juice", "Coffee", "Sandwich", "Chocolate" };
            ViewData["Products"] = products;//ارسلنا الاري الى الفيو
            return View();
        }

        public IActionResult Suppliers()
        {
            ViewBag.PageTitle ="Our Suppliers";
            ViewBag.NumberOfSuppliers = 3;
            string[] suppliers = { "Fresh Food Company", "Happy Drinks Company", "Daily Supplies Company" };
            ViewBag.Suppliers = suppliers;
            return View();
        }
        [HttpGet]
        public IActionResult CreateRequest()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SubmitRequest()
        {
            string employeeName = Request.Form["EmployeeName"];
            string productName = Request.Form["ProductName"];
            int quantity = int.Parse(Request.Form["Quantity"]);
            DateTime requiredDate = DateTime.Parse(Request.Form["RequiredDate"]);
            if (string.IsNullOrEmpty(employeeName))
            {
                ViewBag.Message = "Employee Name is required!";
                return View("CreateRequest");
            }
            TempData["Message"] = "Request submitted successfully!";

            return RedirectToAction("About");
        }
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
    }
}
