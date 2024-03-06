using System;
using HubtelCommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HubtelCommerce.Database
{
	public class AuthenticationContext : IdentityDbContext<User>
	{
		public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
		{
		}
	}
}

