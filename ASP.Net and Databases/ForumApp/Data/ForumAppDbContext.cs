using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ForumApp.Data
{
	public class ForumAppDbContext : DbContext
	{
		private Post FirstPost { get; set; } = null!;

		private Post SecondPost { get; set; } = null!;

		private Post ThirdPost { get; set; } = null!;

		public ForumAppDbContext(DbContextOptions<ForumAppDbContext> options)
		: base(options)
		{
			Database.Migrate();
		}

		public DbSet<Post> Posts { get; init; } = null!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			SeedPosts();

			modelBuilder
				.Entity<Post>()
				.HasData(FirstPost, SecondPost, ThirdPost);

			base.OnModelCreating(modelBuilder);
		}

		private void SeedPosts()
		{
			FirstPost = new Post()
			{
				Id = 1,
				Title = "My first post",
				Content = "Praesent rhoncus luctus ante. Quisque in dolor ut odio efficitur vehicula vel a turpis. Cras ut magna mi. Nam ante."
			};

			SecondPost = new Post()
			{
				Id = 2,
				Title = "My second post",
				Content = "Aliquam erat volutpat. Vestibulum luctus lacus nec diam aliquam egestas. Morbi neque arcu, pharetra eget mi a, lobortis condimentum risus."
			};

			ThirdPost = new Post()
			{
				Id = 3,
				Title = "My third post",
				Content = "Nunc sagittis fringilla orci, nec pharetra dolor faucibus a. Nunc vehicula arcu non purus consectetur sagittis. Nunc volutpat tincidunt nisl."
			};
		}
	}
}
