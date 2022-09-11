using BbWeb.Data;
using BbWeb.Interfaces.Manager;
using BbWeb.Models;
using BbWeb.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BbWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
     
        IShoppingCartManager _shoppingCartManager;
        
        public int OrderTotal { get; set; }

        ShoppingCartVM ShoppingCartVM;
        public CartController(ApplicationDbContext db,IShoppingCartManager shoppingCartManager)
        {
            _db = db;
            _shoppingCartManager = shoppingCartManager;
        }
       
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            
            ShoppingCartVM = new ShoppingCartVM()
            {
                ListCart = _shoppingCartManager.GetAll(u=>u.ApplicationUserId == claim.Value,
                includeProperties : "Product"),
                OrderHeader = new()
            };
            foreach (var cart in ShoppingCartVM.ListCart)
            {
                cart.Price = CalculatePrice(cart.Count, cart.Product.Price);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);



            }
            return View(ShoppingCartVM);
        }


        public IActionResult Checkout()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCartVM = new ShoppingCartVM()
            {
                ListCart = _shoppingCartManager.GetAll(u => u.ApplicationUserId == claim.Value,
                includeProperties: "Product"
                ),
                OrderHeader = new()

            };

            ShoppingCartVM.OrderHeader.ApplicationUser = _db.ApplicationUsers.FirstOrDefault(
                u => u.Id == claim.Value);

            ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.Adress = ShoppingCartVM.OrderHeader.ApplicationUser.Address;
            ShoppingCartVM.OrderHeader.Citty = ShoppingCartVM.OrderHeader.ApplicationUser.CityName;





            foreach (var cart in ShoppingCartVM.ListCart)
            {
                cart.Price = CalculatePrice(cart.Count, cart.Product.Price);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);

            }
            return View(ShoppingCartVM);
        }

        [HttpPost]
        [ActionName("Checkout")]
        [ValidateAntiForgeryToken]
        public IActionResult CheckoutPost(ShoppingCartVM ShoppingCartVM)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVM.ListCart = _shoppingCartManager.GetAll(u => u.ApplicationUserId == claim.Value,
                includeProperties: "Product");

            ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
            ShoppingCartVM.OrderHeader.OrderDate = System.DateTime.Now;
            ShoppingCartVM.OrderHeader.ApplicationUserId = claim.Value;



            foreach (var cart in ShoppingCartVM.ListCart)
            {
                cart.Price = CalculatePrice(cart.Count, cart.Product.Price);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);

            }
            _db.OrderHeaders.Add(ShoppingCartVM.OrderHeader);
            _db.SaveChanges();


            foreach (var cart in ShoppingCartVM.ListCart)
            {
                OrderDetail orderDetail = new()
                {
                    ProductId = cart.ProductId,
                    OrderId = ShoppingCartVM.OrderHeader.Id,
                    Price = cart.Price,
                    Count = cart.Count
                };
                _db.OrderDetails.Add(orderDetail);
                _db.SaveChanges();

            }
            _db.ShoppingCarts.RemoveRange(ShoppingCartVM.ListCart);
            _db.SaveChanges();
            return RedirectToAction("Successful");
        }

        public IActionResult Successful()
        {
            return View();
        }

        public IActionResult Plus(int cartId)
        {
            //var cart = _shoppingCartManager.GetFirstOrDefault(c=>c.Id == cartId);
            //_shoppingCartManager.IncrementCount(cart, 1);
            //_shoppingCartManager.Save();
            //return RedirectToAction(nameof(Index));

            var cart = _db.ShoppingCarts.FirstOrDefault(u => u.Id == cartId);
            cart.Count = IncrementCount(cart, 1);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {

            var cart = _db.ShoppingCarts.FirstOrDefault(u => u.Id == cartId);
            cart.Count = DecrementCount(cart, 1);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {

            var cart = _db.ShoppingCarts.FirstOrDefault(u => u.Id == cartId);
            _db.ShoppingCarts.Remove(cart);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private int IncrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count += count;
            return shoppingCart.Count;
        }

        private int DecrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count -= count;
            return shoppingCart.Count;
        }

        private double CalculatePrice(double quantity,decimal price)
        {
            return ((double)price);
        }        
    }
}
