namespace OpenAIWrapper.Data;

using OpenAIWrapper.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Authentication> Authentications { get; set; }

    // public override void OnModelCreating(ModelBuilder modelBuilder) {
    //     modelBuilder.Entity<User>()
    //         .HasOne(u => u.Authentications)       // One User has one Authentication
    //         .WithOne(a => a.User)                // One Authentication has one User
    //         .HasForeignKey<Authentication>(a => a.Id);  // Specify that Authentication's Id is also the foreign key to User
    // }
}