using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Data.Models;
using Task = TaskBoardApp.Data.Models.Task;

namespace TaskBoardApp.Data
{
	public class TaskBoardAppDbContext : IdentityDbContext<IdentityUser>
	{
		public TaskBoardAppDbContext(DbContextOptions<TaskBoardAppDbContext> options)
			: base(options)
		{
			Database.Migrate();
		}

		public DbSet<Board> Boards { get; set; } = null!;

		public DbSet<Task> Tasks { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder builder)
		{
			SeedUsers();
			builder
				.Entity<IdentityUser>()
				.HasData(TestUser);

			SeedBoards();
			builder
				.Entity<Board>()
				.HasData(OpenBoard, InProgressBoard, DoneBoard);

			builder
				.Entity<Task>()
				.HasData(new Task()
				{
					Id = 1,
					Title = "Improve CSS styles",
					Description = "Implement better styling for all public pages",
					CreatedOn = DateTime.Now.AddDays(-200),
					OwnerId = TestUser.Id,
					BoardId = OpenBoard.Id
				},
					new Task()
					{
						Id = 2,
						Title = "Android Client App",
						Description = "Create Android Client App for the TaskBoard RESTful API",
						CreatedOn = DateTime.Now.AddDays(-5),
						OwnerId = TestUser.Id,
						BoardId = OpenBoard.Id
					},
					new Task()
					{
						Id = 3,
						Title = "Desktop Client App",
						Description = "Create Windows Forms desktop app client for the TaskBoard RESTful API",
						CreatedOn = DateTime.Now.AddMonths(-1),
						OwnerId = TestUser.Id,
						BoardId = InProgressBoard.Id
					},
					new Task()
					{
						Id = 4,
						Title = "Create Tasks",
						Description = "Implement [Create Task] page for adding new tasks",
						CreatedOn = DateTime.Now.AddYears(-1),
						OwnerId = TestUser.Id,
						BoardId = DoneBoard.Id
					});

			builder
				.Entity<Task>()
				.HasOne(t => t.Board)
				.WithMany(b => b.Tasks)
				.HasForeignKey(t => t.BoardId)
				.OnDelete(DeleteBehavior.Restrict);

			base.OnModelCreating(builder);
		}

		private IdentityUser TestUser { get; set; }

		private Board OpenBoard { get; set; }

		private Board InProgressBoard { get; set; }

		private Board DoneBoard { get; set; }

		private void SeedUsers()
		{
			var hasher = new PasswordHasher<IdentityUser>();

			TestUser = new IdentityUser()
			{
				UserName = "test@softuni.bg",
				NormalizedUserName = "TEST@SOFTUNI.BG"
			};

			TestUser.PasswordHash = hasher.HashPassword(TestUser, "softuni");
		}

		private void SeedBoards()
		{
			OpenBoard = new Board()
			{
				Id = 1,
				Name = "Open"
			};

			InProgressBoard = new Board()
			{
				Id = 2,
				Name = "In Progress"
			};

			DoneBoard = new Board()
			{
				Id = 3,
				Name = "Done"
			};
		}
	}
}