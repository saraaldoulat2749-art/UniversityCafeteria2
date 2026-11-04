using Microsoft.AspNetCore.Mvc;
using UniversityCafeteria.Models;

namespace UniversityCafeteria.Controllers
{
    public class SupplyRequestController : Controller
    {
        static List<SupplyRequest> requests = new List<SupplyRequest>();

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Products = HttpContext.Session.GetString("Products");
            ViewBag.Priority = Request.Cookies["PreferredPriority"];
            return View();
        }

        [HttpPost]
        public IActionResult AddItem(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.ProductName))
            {
                ViewBag.Message = "Product Name is required";
            }
            else if (product.Quantity <= 0)
            {
                ViewBag.Message = "Invalid Quantity";
            }
            else if (product.UnitPrice <= 0)
            {
                ViewBag.Message = "Invalid Price";
            }
            else
            {
                string products = HttpContext.Session.GetString("Products");

                products += product.ProductName + " - "
                          + product.Category + " - "
                          + product.Quantity + " - "
                          + product.UnitPrice + " - "
                          + product.Priority + "\n";

                HttpContext.Session.SetString("Products", products);

                ViewBag.Message = "Product added successfully";
            }

            ViewBag.Products = HttpContext.Session.GetString("Products");

            return View("Index");
        }
        [HttpGet]
        public IActionResult Review()
        {
            string products = HttpContext.Session.GetString("Products");

            decimal subtotal = 0;
            decimal fee = 0;
            decimal discount = 0;
            string priority = "Normal";

            if (!string.IsNullOrEmpty(products))
            {
                string[] lines = products.Split('\n');

                foreach (string line in lines)
                {
                    if (line == "")
                        continue;

                    string[] data = line.Split(" - ");

                    int quantity = int.Parse(data[2]);
                    decimal price = decimal.Parse(data[3]);

                    subtotal += quantity * price;
                    priority = data[4];
                }
            }

            switch (priority)
            {
                case "Normal":
                    fee = 2;
                    break;
                case "Urgent":
                    fee = 5;
                    break;
                case "Emergency":
                    fee = 10;
                    break;
            }

            if (subtotal > 200)
                discount = subtotal * 0.15m;
            else if (subtotal > 100)
                discount = subtotal * 0.10m;

            decimal total = subtotal + fee - discount;

            ViewBag.Products = products;
            ViewBag.Subtotal = subtotal;
            ViewBag.Fee = fee;
            ViewBag.Discount = discount;
            ViewBag.Total = total;

            return View();
        }
        [HttpPost]
        [HttpPost]
        public IActionResult ConfirmRequest()
        {
            string products = HttpContext.Session.GetString("Products");

            if (string.IsNullOrEmpty(products))
            {
                TempData["Message"] = "Request is empty";
                return RedirectToAction("Review");
            }

            string[] lines = products.Split('\n');

            int count = 0;
            decimal subtotal = 0;
            string priority = "Normal";

            foreach (string line in lines)
            {
                if (line != "")
                {
                    string[] data = line.Split(" - ");

                    if (data.Length == 5)
                    {
                        count++;

                        int quantity = int.Parse(data[2]);
                        decimal price = decimal.Parse(data[3]);

                        subtotal += quantity * price;
                        priority = data[4];
                    }
                }
            }

            if (count == 0)
            {
                TempData["Message"] = "Request is empty";
                return RedirectToAction("Review");
            }

            decimal fee = 0;
            decimal discount = 0;

            switch (priority)
            {
                case "Normal":
                    fee = 2;
                    break;

                case "Urgent":
                    fee = 5;
                    break;

                case "Emergency":
                    fee = 10;
                    break;
            }

            if (subtotal > 200)
            {
                discount = subtotal * 0.15m;
            }
            else if (subtotal > 100)
            {
                discount = subtotal * 0.10m;
            }

            SupplyRequest request = new SupplyRequest();

            request.RequestID = requests.Count + 1;
            request.NumberOfProducts = count;
            request.Subtotal = subtotal;
            request.Fee = fee;
            request.Discount = discount;
            request.Total = subtotal + fee - discount;
            request.Status = "Confirmed";

            requests.Add(request);

            HttpContext.Session.Remove("Products");

            TempData["Message"] = "Request confirmed successfully";

            return RedirectToAction("MyRequests");
        }

        [HttpPost]
        public IActionResult ClearDraft()
        {
            HttpContext.Session.Remove("Products");

            return RedirectToAction("Index");
        }

        
        [HttpPost]
        public IActionResult SavePreference(string priority)
        {
            Response.Cookies.Append("PreferredPriority", priority,
                new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddDays(7)
                });

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult MyRequests()
        {
            ViewBag.Requests = requests;

            return View();
        }
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
    }
}
