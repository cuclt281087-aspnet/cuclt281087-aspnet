using Microsoft.EntityFrameworkCore;
using ShoeStore.Data;
using ShoeStore.Models;

namespace ShoeStore.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync(string sessionId, string? customerId = null)
        {
            var query = _context.CartItems.Include(c => c.Product)
                .Where(c => c.SessionId == sessionId);

            return await query.ToListAsync();
        }

        public async Task<CartItem> AddToCartAsync(string sessionId, string? customerId, Guid productId, int size, string color, int quantity = 1)
        {
            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(c => 
                    c.SessionId == sessionId &&
                    c.ProductId == productId && 
                    c.Size == size && 
                    c.Color == color);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                await _context.SaveChangesAsync();
                return existingItem;
            }

            var cartItem = new CartItem
            {
                SessionId = sessionId,
                ProductId = productId,
                Size = size,
                Color = color,
                Quantity = quantity
            };

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task UpdateCartItemAsync(Guid cartItemId, int quantity)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                if (quantity <= 0)
                {
                    _context.CartItems.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity = quantity;
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveFromCartAsync(Guid cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(string sessionId, string? customerId = null)
        {
            var items = await _context.CartItems
                .Where(c => c.SessionId == sessionId)
                .ToListAsync();
            
            _context.CartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetCartTotalAsync(string sessionId, string? customerId = null)
        {
            var items = await GetCartItemsAsync(sessionId, customerId);
            return items.Sum(item => (item.Product.SalePrice ?? item.Product.Price) * item.Quantity);
        }

        public async Task<int> GetCartItemCountAsync(string sessionId, string? customerId = null)
        {
            var items = await GetCartItemsAsync(sessionId, customerId);
            return items.Sum(item => item.Quantity);
        }
    }
}