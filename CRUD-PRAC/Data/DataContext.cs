using CRUD_PRAC.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_PRAC.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Member> TempPlayers { get; set; }
        public DbSet<Availablity> TempAvailablities { get; set; }
    }
}
