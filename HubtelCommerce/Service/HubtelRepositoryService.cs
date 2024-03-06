using System;
using HubtelCommerce.Database;
using HubtelCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace HubtelCommerce.Service
{
	public class HubtelRepositoryService : IHubtelRepositoryService
	{
        private readonly DatabaseContext _dbContext;
		public HubtelRepositoryService(DatabaseContext context)
		{
            _dbContext = context;
		}

        public Task<Cart> AddOrUpdateItemsToCartAsync(Cart cart)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Cart>> GetAllCartItemsAsync(string userId)
        {
            if (!string.IsNullOrWhiteSpace(userId))
            {
                var itemsInCart = await _dbContext.Carts
                    .Where(x => x.CustomerId == userId)
                    .ToListAsync();

                return itemsInCart;
            }
            return Enumerable.Empty<Cart>();
        }

        public async Task<Cart?> GetSingleItemAsync(string itemId, string customerId)
        {
            var item = new Cart();
            if(!string.IsNullOrEmpty(itemId) && !string.IsNullOrEmpty(customerId))
            {
                item = await _dbContext.Carts
                    .Where(item => item.ItemId == itemId && item.CartId == customerId)
                    .SingleOrDefaultAsync();
            }
            return item;
        }

        public async Task RemoveItemAsync(string itemId, string customerId)
        {
            if (!string.IsNullOrWhiteSpace(itemId) && !string.IsNullOrWhiteSpace(customerId))
            {
                var itemToRemove = await _dbContext.Carts.
                                Where(item => item.ItemId == itemId && item.CustomerId == customerId)
                                .SingleOrDefaultAsync();
                if (itemToRemove is not null)
                {
                    _dbContext.Remove(itemToRemove);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}

