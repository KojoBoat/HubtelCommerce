using System;
using HubtelCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace HubtelCommerce.Database
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base (options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Cart> Carts { get; set; }

		//Seed the Db
	}
}

