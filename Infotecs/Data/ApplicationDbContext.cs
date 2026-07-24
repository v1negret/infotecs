using Infotecs.Models;
using Infotecs.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infotecs.Data;

public class ApplicationDbContext : DbContext
{
    
    public DbSet<FileEntity> Files { get; set; }
    public DbSet<ValueEntity>  Values { get; set; }
    public DbSet<ResultEntity>  Results { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FileEntity>()
            .HasKey(f => f.Uid);
        modelBuilder.Entity<ValueEntity>()
            .HasKey(v => v.Uid);
        modelBuilder.Entity<ResultEntity>()
            .HasKey(r => r.Uid);
    }
}