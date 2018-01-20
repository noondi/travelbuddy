using Microsoft.EntityFrameworkCore;
 
namespace newProject.Models
{
    public class TravelContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public TravelContext(DbContextOptions<TravelContext> options) : base(options) { }
  
        // This DbSet contains "Person" objects and is called "Users"
        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Plan> Plans { get; set; }
       
    }
}