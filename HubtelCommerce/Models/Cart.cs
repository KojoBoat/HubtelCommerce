using System;
namespace HubtelCommerce.Models
{
	public class Cart : BaseModel
	{
		public string? CustomerId { get; set; }
		public string? CartId { get; set; }
		public string? ItemId { get; set; }
		public int CustomerTelNumber { get; set; }
		public DateTime Time { get; set; }
	}
}

