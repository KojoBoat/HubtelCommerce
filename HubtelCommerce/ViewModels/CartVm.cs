using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HubtelCommerce.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HubtelCommerce.ViewModels
{
	public class CartVm
	{
        public string? CartId { get; set; }
        [Required]
        public string? ItemId { get; set; }
        public string? CustomerId { get; set; }
        [Required]
        public string? CustomerTelNumber { get; set; }
        public DateTime Time { get; set; }
        [Required]
        public string? ItemName { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
    }
}

