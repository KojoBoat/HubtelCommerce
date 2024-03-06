using System;
using HubtelCommerce.Database;
using HubtelCommerce.Helpers;
using HubtelCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace HubtelCommerce.Service
{
	public class HubtelRepositoryService : IHubtelRepositoryService
	{
        private readonly DatabaseContext _dbContext;
        private readonly IGuidGenerator _guidGen;

		public HubtelRepositoryService(DatabaseContext context, IGuidGenerator guid)
		{
            _dbContext = context;
            _guidGen = guid;
		}

        public async Task<Cart> AddOrUpdateItemsToCartAsync(Cart cart)
        {
            var cartItem = new Cart();
            if (string.IsNullOrEmpty(cart.CartId))
            {
                cart.CartId = _guidGen.GenerateGuid();
                await _dbContext.AddAsync(cart);
            }
            else
            {
                 cartItem = await _dbContext.Carts
                .Where(item => item.CartId == cart.CartId && item.ItemId == cart.ItemId)
                .SingleOrDefaultAsync();

                if (cartItem is not null)
                {
                    cartItem.Quantity += cart.Quantity;
                    _dbContext.Update(cartItem);
                }
                else
                {
                    await _dbContext.AddAsync(cart);
                }
            }

            await _dbContext.SaveChangesAsync();
            return cartItem!;
        }

        public async Task<IEnumerable<Cart>> GetAllCartItemsAsync(string cartId, string userId)
        {
            if (!string.IsNullOrWhiteSpace(userId) && !string.IsNullOrWhiteSpace(cartId))
            {
                var itemsInCart = await _dbContext.Carts
                    .Where(x => x.CustomerId == userId && x.CartId == cartId)
                    .ToListAsync();

                return itemsInCart;
            }
            return Enumerable.Empty<Cart>();
        }

        public async Task<Cart?> GetSingleItemAsync(string cartId, string itemId, string userId)
        {
            var item = new Cart();
            if(!string.IsNullOrEmpty(itemId) && !string.IsNullOrEmpty(userId))
            {
                item = await _dbContext.Carts
                    .Where(item => item.ItemId == itemId && item.CartId == cartId && item.CustomerId == userId)
                    .SingleOrDefaultAsync();
            }
            return item;
        }

        public async Task RemoveItemAsync(string cartId, string itemId, string userId)
        {
            if (!string.IsNullOrWhiteSpace(itemId) && !string.IsNullOrWhiteSpace(userId))
            {
                var itemToRemove = await _dbContext.Carts.
                                Where(item => item.ItemId == itemId && item.CartId == cartId && item.CustomerId == userId)
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

