using BbWeb.Data;
using BbWeb.Interfaces.Manager;
using BbWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using X.PagedList;

namespace BbWeb.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        IShoppingCartManager _shoppingCartManager;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext db,IShoppingCartManager shoppingCartManager)
        {
            _logger = logger;
            _db = db;
            _shoppingCartManager = shoppingCartManager;
        }

        public IActionResult Index(int? page,string SearchText = "")
        {
            List<Product> product;
            SearchText = SearchText.ToLower();
            if (SearchText != "" && SearchText != null)
            {
                product = _db.Products.Include(c => c.Category).Include(x => x.Tag).
                    Where(c => c.ProductName.ToLower().Contains(SearchText) || c.Description.ToLower().Contains(SearchText) ||
                    c.Category.CategoryName.ToLower().Contains(SearchText) || c.Tag.TagName.ToLower().Contains(SearchText)).ToList();
            }
            else
            product = _db.Products.Include(c => c.Category).Include(c => c.Tag).ToList();
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // get product details

        public IActionResult Details(int productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            //var product = _db.Products.Include(c => c.Category).Include(c => c.Tag).FirstOrDefault(c => c.Id == id);
            //if(product == null)
            //{
            //    return NotFound();
            //}
            ShoppingCart cart = new()
            {
                Count = 1,
                ProductId = productId,
                Product = _db.Products.Include(c => c.Category).Include(c => c.Tag).FirstOrDefault(c => c.Id == productId),

        };

            return View(cart);
        }

        // post details action
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart cart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            cart.ApplicationUserId = claim.Value;

            ShoppingCart cartFromDb = _shoppingCartManager.GetFirstOrDefault(
                 u => u.ApplicationUserId == claim.Value && u.ProductId == cart.ProductId);

            if (cartFromDb == null)
            {
                _shoppingCartManager.Add(cart);

            }
            else
            {
                _shoppingCartManager.IncrementCount(cartFromDb, cart.Count);
            }

            _shoppingCartManager.Save();
            
            
            return RedirectToAction(nameof(Index));
        }
    }
}