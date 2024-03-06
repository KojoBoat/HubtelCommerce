using System;
using Microsoft.AspNetCore.Mvc;

namespace HubtelCommerce.FiltersModel
{
    public class CartFilterModel
    {
        [FromQuery]
        public string? PhoneNumber { get; set; }
        [FromQuery]
        public DateTime? TimeCreated { get; set; }
        [FromQuery]
        public int? Quantity { get; set; }
        [FromQuery]
        public string? ItemName { get; set; }
    }
}

