using Microsoft.EntityFrameworkCore;
using ShoppingListApp.Data.Models;

namespace ShoppingListApp.Data
{
	public class ShoppingListAppDbContext : DbContext
	{
		private Product firstProduct { get; set; } = null!;
		private Product secondProduct { get; set; } = null!;
		private Product thirdProduct { get; set; } = null!;

		public ShoppingListAppDbContext(DbContextOptions<ShoppingListAppDbContext> options)
			: base(options)
		{

		}

		public DbSet<Product> Products { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			SeedProducts();

			modelBuilder.Entity<Product>()
				.HasData(firstProduct, secondProduct, thirdProduct);

			base.OnModelCreating(modelBuilder);
		}

		private void SeedProducts()
		{
			firstProduct = new Product()
			{
				Id = 1,
				Name = "Cheese"
			};

			secondProduct = new Product()
			{
				Id = 2,
				Name = "Ham"
			};

			thirdProduct = new Product()
			{
				Id = 3,
				Name = "Mustard"
			};
		}
	}
}
