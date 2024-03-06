using System;
using System.ComponentModel.DataAnnotations;

namespace HubtelCommerce.Models
{
	public class Login
	{
		[Required(ErrorMessage = "Username is required!")]
		public string? UserName { get; set; }

		[Required(ErrorMessage = "Please provide your password")]
		public string? Password { get; set; }
	}
}

