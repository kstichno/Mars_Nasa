using Microsoft.EntityFrameworkCore;
using MarsNasa.Models;

namespace MarsNasa.DataAccess
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ImageOfTheDay> Image { get; set; }

    public DbSet<Weather> Weather { get; set; }

    public DbSet<MarsRoverImage> RoverImage { get; set; }

    public DbSet<MarsCamera> Camera { get; set; }

    public DbSet<Photo> Photo { get; set; }

    }
}