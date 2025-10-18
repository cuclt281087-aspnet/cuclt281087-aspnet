using ShoeStore.Models;

namespace ShoeStore.Services
{
    public interface ICartService
    {
        Task<IEnumerable<CartItem>> GetCartItemsAsync(string sessionId, string? customerId = null);
        Task<CartItem> AddToCartAsync(string sessionId, string? customerId, Guid productId, int size, string color, int quantity = 1);
        Task UpdateCartItemAsync(Guid cartItemId, int quantity);
        Task RemoveFromCartAsync(Guid cartItemId);
        Task ClearCartAsync(string sessionId, string? customerId = null);
        Task<decimal> GetCartTotalAsync(string sessionId, string? customerId = null);
        Task<int> GetCartItemCountAsync(string sessionId, string? customerId = null);
    }
}