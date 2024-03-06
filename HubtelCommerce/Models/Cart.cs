using System;
namespace HubtelCommerce.Models
{
	public class Cart : BaseModel
	{
		public string? CustomerId { get; set; }
		public string? CartId { get; set; }
		public string? ItemId { get; set; }
		public string? CustomerTelNumber { get; set; }
		public DateTime Time { get; set; }
	}
}

