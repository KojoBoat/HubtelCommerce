using System;
using HubtelCommerce.FiltersModel;
using HubtelCommerce.Helpers;
using HubtelCommerce.Models;
using HubtelCommerce.Service;
using HubtelCommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HubtelCommerce.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/v1/[controller]")]
	public class CartController : ControllerBase
	{
		private readonly IHubtelRepositoryService _repoService;
		private readonly UserIdHelper _userId;
		private readonly ILogger<CartController> _logger;
		private readonly IGuidGenerator _guid;

		public CartController(IHubtelRepositoryService repo, UserIdHelper helper,
			ILogger<CartController> logger, IGuidGenerator guid)
		{
			_repoService = repo;
			_userId = helper;
			_logger = logger;
			_guid = guid;
		}

		[HttpPost]
		public async Task<IActionResult> AddItemToCart(CartVm? cart)
		{
			if (cart is null) return BadRequest();
			try
			{
				var itemToAdd = new Cart
				{
					CartId = cart.CartId,
					CustomerId = _userId.GetCustomerId(),
					CustomerTelNumber = cart.CustomerTelNumber,
					ItemId = cart.ItemId,
					ItemName = cart.ItemName,
					Quantity = cart.Quantity,
					Time = DateTime.UtcNow,
					UnitPrice = cart.UnitPrice
				};

				var result = await _repoService.AddOrUpdateItemsToCartAsync(itemToAdd);
				return Ok(result);
			}
			catch(Exception ex)
			{
				_logger.LogError(ex.InnerException, "Error trying to add item to cart");
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpGet("GetAllItems/{cartId}")]
		public async Task<IActionResult> GetAllItemsInCartAsync(string cartId, [FromQuery] CartFilterModel model)
		{
			try
			{
				var results = await _repoService.GetAllCartItemsAsync(cartId, _userId.GetCustomerId(), model);
				if (results is null)
					return NotFound("Cart is Empty");
				return Ok(results);
            }
			catch (Exception ex)
			{
				_logger.LogError(ex.InnerException, "Failed to load Cart items.");
				return NotFound();
			}
		}

		[HttpGet("item/{itemId}/cart/{cartId}")]
		public async Task<IActionResult> GetItemById(string itemId, string cartId)
		{
			if (string.IsNullOrEmpty(itemId) || string.IsNullOrEmpty(cartId)) return BadRequest();
			try
			{
				var result = await _repoService.GetSingleItemAsync(cartId, itemId, _userId.GetCustomerId());
				if (result is null) return NotFound($"No item with Id {itemId} was found.");
				return Ok(result);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.InnerException, "An error occured retrieving Item from cart");
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpDelete("item/{itemId}/cart/{cartId}")]
		public async Task<IActionResult> DeleteItemFromCart(string itemId, string cartId)
		{
			if (string.IsNullOrEmpty(itemId) || string.IsNullOrEmpty(cartId)) return BadRequest("Parameters cannot be null!");
			try
			{
				await _repoService.RemoveItemAsync(cartId, itemId, _userId.GetCustomerId());
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.InnerException, "Deleting item wasn't succesful");
				return BadRequest();
			}
		}
	}
}

