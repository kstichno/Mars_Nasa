using Microsoft.EntityFrameworkCore;
using MarsNasa.Models;

namespace MarsNasa.DataAccess
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<ImageOfTheDay> Image { get; set; }
  }
}