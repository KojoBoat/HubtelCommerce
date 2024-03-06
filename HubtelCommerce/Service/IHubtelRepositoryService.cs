using System;
using HubtelCommerce.Models;

namespace HubtelCommerce.Service
{
	public interface IHubtelRepositoryService
	{
		public Task<Cart> AddOrUpdateItemsToCartAsync(Cart cart);
		public Task<IEnumerable<Cart>> GetAllCartItemsAsync(string customerId);
		public Task RemoveItemAsync(string itemId, string customerId);
		public Task<Cart?> GetSingleItemAsync(string itemId, string customerId);
	}
}

