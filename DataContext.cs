using CrocusFitnes.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrocusFitnes;

public class DataContext:DbContext
{
      public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Members> Members { get; set; }
    public DbSet<Trainers> Trainers { get; set; }
    public DbSet<Session> Session { get; set; }
    
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
}