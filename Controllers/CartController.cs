using Microsoft.AspNetCore.Mvc;
using ShoeStore.Services;
using ShoeStore.Models;

namespace ShoeStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var sessionId = HttpContext.Session.Id;
            var userId = HttpContext.Session.GetString("UserId");
            
            var cartItems = await _cartService.GetCartItemsAsync(sessionId, userId);
            var total = await _cartService.GetCartTotalAsync(sessionId, userId);
            
            ViewBag.Total = total;
            return View(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productId, int size, string color, int quantity = 1)
        {
            var sessionId = HttpContext.Session.Id;
            var userId = HttpContext.Session.GetString("UserId");
            
            await _cartService.AddToCartAsync(sessionId, userId, productId, size, color, quantity);
            
            return Json(new { success = true, message = "Đã thêm vào giỏ hàng" });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(Guid cartItemId, int quantity)
        {
            await _cartService.UpdateCartItemAsync(cartItemId, quantity);
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(Guid cartItemId)
        {
            await _cartService.RemoveFromCartAsync(cartItemId);
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            var sessionId = HttpContext.Session.Id;
            var userId = HttpContext.Session.GetString("UserId");
            
            await _cartService.ClearCartAsync(sessionId, userId);
            return Json(new { success = true });
        }

        public async Task<IActionResult> GetCartCount()
        {
            var sessionId = HttpContext.Session.Id;
            var userId = HttpContext.Session.GetString("UserId");
            
            var count = await _cartService.GetCartItemCountAsync(sessionId, userId);
            return Json(new { count });
        }
    }
}