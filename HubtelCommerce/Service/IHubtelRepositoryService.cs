using System;
using HubtelCommerce.FiltersModel;
using HubtelCommerce.Models;

namespace HubtelCommerce.Service
{
	public interface IHubtelRepositoryService
	{
		public Task<Cart> AddOrUpdateItemsToCartAsync(Cart cart);
		public Task<IEnumerable<Cart>> GetAllCartItemsAsync(string cartId, string userId, CartFilterModel? model);
		public Task RemoveItemAsync(string cartId, string itemId, string userId);
		public Task<Cart?> GetSingleItemAsync(string cartId, string itemId, string userId);
	}
}

