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


        [Authorize]
        public IActionResult FarmManager() {
        
            return View();
        }

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

        [Authorize]
        public async Task<IActionResult> ShowAllFarmer()
        {
            if (_context.Farmers == null)
            {
                throw new Exception("Farmers table is null. Check your database configuration.");
            } 

            var farmers = await _context.Farmers.ToListAsync();
            return View("Index",farmers); 
           
        }

       // public ActionResult ShowAllProducts()
       // {
            // Retrieve all products from the database
           // var products = await _context.Products.ToListAsync();

            // Pass the list of products to the view
            //return View(products);
      //  }


        [Authorize]
        public IActionResult FarmerLogin()
        {

            return View();
        }



        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginFarmer(FarmerSignIn model)
        {
            if (ModelState.IsValid)
            {
                // Check if a farmer exists with the entered name and password
                var farmer = await _context.Farmers
                    .FirstOrDefaultAsync(f => f.FName == model.FName && f.FPassword == model.FPassword);

                if (farmer == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid farmer name or password.");
                    return View("Index");
                }

                // If the farmer is found, store their ID and redirect to the products management page
                TempData["FarmerId"] = farmer.Id;
                return RedirectToAction("ViewProduct"); // The product management page
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            // Log errors (e.g., to console or debug output)
            System.Diagnostics.Debug.WriteLine(string.Join("; ", errors));
           

            // If model state is invalid, return the view with errors
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
            var farmerIdTemp = TempData.Peek("FarmerId");
           // !int.TryParse(TempData["FarmerId"].ToString(), out int farmerId);
          //  !await _context.Farmers.AnyAsync(f => f.Id == farmerId);


            if (ModelState.IsValid)
            {
                int? num = 7;
                var product = new Product
                {

                    
                    PName = model.PName,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    FarmerId = model.FarmerId
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return RedirectToAction("ViewProduct");
            }

            return View("Index", model);
        }

        //-----------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------

        [Authorize]
        public IActionResult PromptProduct()
        {
            return View("AddProduct");
        }




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
    }
}
