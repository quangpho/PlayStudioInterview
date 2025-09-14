using Microsoft.EntityFrameworkCore;
using Model;

namespace Database;

public class ClubsDbContext : DbContext 
{
    public ClubsDbContext(DbContextOptions<ClubsDbContext> options) 
        : base(options)
    {
    }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<Player> Players { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Club>()
            .HasIndex(c => c.Name)
            .IsUnique();

        modelBuilder.Entity<Player>()
            .HasOne(p => p.Club)
            .WithMany(c => c.Members);
        
        modelBuilder.Entity<Player>()
            .Property(p => p.PlayerId)
            .ValueGeneratedNever();
        
        modelBuilder.Entity<Player>()
            .HasIndex(c => c.PlayerId)
            .IsUnique();
    }
}