using Microsoft.AspNetCore.Mvc;

namespace UniversityCafeteria.Controllers
{
    public class SupplyController : Controller
    {
        public IActionResult Index()
        {
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
            ViewBag.NumberOfProducts = 3;
            string[] suppliers = { "Fresh Food Company", "Happy Drinks Company", "Daily Supplies Company" };
            ViewBag.Suppliers = suppliers;
            return View();
        }

        public IActionResult CreateRequest()
        {
            return View();
        }

        public IActionResult SubmitRequest()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
