namespace Game2048.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class Game2048DbContext : DbContext
    {
        public Game2048DbContext(DbContextOptions<Game2048DbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Score> Scores { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<User>()
                .HasMany(u => u.Scores)
                .WithOne(sc => sc.User)
                .HasForeignKey(sc => sc.UserId);

            base.OnModelCreating(builder);
        }
    }
}

