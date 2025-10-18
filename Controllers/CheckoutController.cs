using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Data;
using ShoeStore.Models;
using ShoeStore.Services;

namespace ShoeStore.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartService _cartService;

        public CheckoutController(ApplicationDbContext context, ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var sessionId = HttpContext.Session.Id;
            var userId = HttpContext.Session.GetString("UserId");
            
            var cartItems = await _cartService.GetCartItemsAsync(sessionId, userId);
            var total = await _cartService.GetCartTotalAsync(sessionId, userId);
            
            if (!cartItems.Any())
            {
                return RedirectToAction("Index", "Cart");
            }
            
            ViewBag.CartItems = cartItems;
            ViewBag.Total = total;
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(Order order)
        {
            var sessionId = HttpContext.Session.Id;
            var userId = HttpContext.Session.GetString("UserId");
            
            var cartItems = await _cartService.GetCartItemsAsync(sessionId, userId);
            
            if (!cartItems.Any())
            {
                return RedirectToAction("Index", "Cart");
            }
            
            order.Id = Guid.NewGuid();
            order.OrderNumber = "ORD" + DateTime.Now.ToString("yyyyMMddHHmmss");
            order.CreatedAt = DateTime.UtcNow;
            order.Status = "Pending";
            order.TotalAmount = await _cartService.GetCartTotalAsync(sessionId, userId);
            
            if (!string.IsNullOrEmpty(userId))
            {
                order.UserId = Guid.Parse(userId);
            }
            
            _context.Orders.Add(order);
            
            foreach (var cartItem in cartItems)
            {
                var orderItem = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Product.SalePrice ?? cartItem.Product.Price,
                    TotalPrice = (cartItem.Product.SalePrice ?? cartItem.Product.Price) * cartItem.Quantity,
                    Size = cartItem.Size,
                    Color = cartItem.Color
                };
                _context.OrderItems.Add(orderItem);
            }
            
            await _context.SaveChangesAsync();
            await _cartService.ClearCartAsync(sessionId, userId);
            
            return RedirectToAction("Success", new { orderId = order.Id });
        }

        public async Task<IActionResult> Success(Guid orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
                
            if (order == null)
            {
                return NotFound();
            }
            
            return View(order);
        }
    }
}