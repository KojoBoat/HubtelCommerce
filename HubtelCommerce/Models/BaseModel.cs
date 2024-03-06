using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace HubtelCommerce.Models
{
	public class BaseModel
	{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? ItemName { get; set; }
		public int Quantity { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(6,2)")]
        public decimal UnitPrice { get; set; }
	}
}
