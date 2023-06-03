using Microsoft.EntityFrameworkCore;
using ShoppingListApp.Models.Product;

namespace ShoppingListApp.Data
{
	public class ShoppingListAppDbContext : DbContext
	{
		public ShoppingListAppDbContext(DbContextOptions<ShoppingListAppDbContext> options)
		:base(options)
		{
				
		}

		public DbSet<ProductNote> ProductNotes { get; set; }

		public DbSet<ProductViewModel> ProductViewModel { get; set; }
	}
}
