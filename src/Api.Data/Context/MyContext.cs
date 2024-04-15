using Api.Domain.Entities;
using Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class MyContext : DbContext
{
    public DbSet<UserEntity> User { get; set; }

    public MyContext(DbContextOptions<MyContext>options) : base(options) { }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserEntity> (new UserMap ().Configure);
    }
    
}