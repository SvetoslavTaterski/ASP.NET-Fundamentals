using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Homies.Data.Entities;
using Type = Homies.Data.Entities.Type;

namespace Homies.Data
{
    public class HomiesDbContext : IdentityDbContext
    {
        public HomiesDbContext(DbContextOptions<HomiesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; } = null!;

        public DbSet<EventParticipant> EventsParticipants { get; set; } = null!;

        public DbSet<Type> Types { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Type>()
                .HasData(new Type()
                {
                    Id = 1,
                    Name = "Animals"
                },
                new Type()
                {
                    Id = 2,
                    Name = "Fun"
                },
                new Type()
                {
                    Id = 3,
                    Name = "Discussion"
                },
                new Type()
                {
                    Id = 4,
                    Name = "Work"
                });

            modelBuilder.Entity<EventParticipant>()
	            .HasKey(k => new
	            {
		            k.HelperId,
		            k.EventId
	            });

            modelBuilder.Entity<EventParticipant>()
	            .HasOne(x => x.Event)
	            .WithMany(x => x.EventsParticipants)
	            .OnDelete(DeleteBehavior.NoAction);
	            


            base.OnModelCreating(modelBuilder);
        }
    }
}