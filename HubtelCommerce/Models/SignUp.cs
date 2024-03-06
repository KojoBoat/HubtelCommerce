using System;
using System.ComponentModel.DataAnnotations;

namespace HubtelCommerce.Models
{
	public class SignUp
	{
		[Required(ErrorMessage = "Please provide your username")]
		public string? UserName { get; set; }

		[EmailAddress]
        [Required(ErrorMessage = "Please provide your email")]
        public string? Email { get; set; }

		[DataType(DataType.Password)]
        [Required(ErrorMessage = "Please provide your password")]
        public string? Password { get; set; }
	}
}

