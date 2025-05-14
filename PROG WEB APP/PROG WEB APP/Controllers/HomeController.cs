using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG_WEB_APP.DATA;
using PROG_WEB_APP.Models;
using PROG_WEB_APP.ViewModels;
using System.Diagnostics;

namespace PROG_WEB_APP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AppDbContext _context;

        public HomeController(AppDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }



        public IActionResult Index()
        {
            return View();
        }
        //---------------------------------------------------------------------



        [Authorize]
        public IActionResult FarmManager()
        {

            return View();
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        // POST: Farmers/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FarmerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var farmer = new Farmer
                {
                    FName = model.FName,
                    FAddress = model.FAddress,
                    FEmail = model.FEmail,
                    FPassword = model.FPassword
                };

                _context.Farmers.Add(farmer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home"); // Adjust redirect as needed
            }

            return View(model);
        }
        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        [Authorize]
        public async Task<IActionResult> ShowAllFarmer()
        {
            if (_context.Farmers == null)
            {
                throw new Exception("Please sign in first or enter the correct details.");
            }

            var farmers = await _context.Farmers.ToListAsync();
            return View("Index", farmers);

        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------



        public IActionResult FarmerLogin()
        {

            return View();
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginFarmer(FarmerSignIn model)
        {
            Console.WriteLine("login farmer");
            if (ModelState.IsValid)
            {
                var farmer = await _context.Farmers
                    .FirstOrDefaultAsync(f => f.FName == model.FName && f.FPassword == model.FPassword);

                if (farmer == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid farmer name or password.");
                    return View("Index");
                }

                // Store the farmer's ID in the session
                HttpContext.Session.SetInt32("FarmerId", farmer.Id);

                return RedirectToAction("ViewProduct");
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            System.Diagnostics.Debug.WriteLine(string.Join("; ", errors));

            return View("Register");
          
        }



        public IActionResult ViewProduct()
        {

            return View();
        }

        //-----------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var farmerId = HttpContext.Session.GetInt32("FarmerId");
                if (farmerId == null)
                {
                    // Farmer not logged in; redirect to login page
                    return RedirectToAction("Login", "Farmer");
                }

                var product = new Product
                {
                    PName = model.PName,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    FarmerId = farmerId.Value,
                    Date = model.Date
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return RedirectToAction("ViewProduct");
            }

            return View(model);
        }

        //-----------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------


        public IActionResult PromptProduct()
        {
            return View("AddProduct");
        }

        //-----------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------




        [Authorize]
        public IActionResult ShowProducts()
        {

            return View();
        }

        //-----------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(FarmerSearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Home");
            }

            var farmer = await _context.Farmers
     .Include(f => f.Products)
     .FirstOrDefaultAsync(f => f.FName.ToLower() == model.FName.ToLower());

            if (farmer == null)
            {
                ModelState.AddModelError(string.Empty, $"No farmer found with the name '{model.FName}'.");
                return View("Farmer is null");
            }

            return View("ProductsByFarmerName", farmer.Products);
        }


        //-----------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchByProductDate(FarmerSearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var products = await _context.Products
                 .Include(p => p.Farmer)
                 .Where(p => p.Date.Date == model.Date.Date) // Direct Date comparison
                 .ToListAsync();

                if (!products.Any())
                {
                    ModelState.AddModelError(string.Empty, $"No products found for the specified date.");
                    return View("ProductsByDate", products);
                }

                return View("ProductsByDate", products);
            }
            return View("ProductsByDate");
            // Direct date comparison without HasValue

        }


        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //-------------------------------------------------------------------------
        //---------------------------------------------------------------------
        [Authorize]
        public IActionResult ShowProductsByDate()
        {

            return View();
        }
    }
}

