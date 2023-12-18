using Fish_Farm.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fish_Farm.Data
{
    public class DataContext : DbContext
    {
        public DataContext( DbContextOptions<DataContext> options ) : base( options ) { }

        public DbSet<User> UserTable { get; set; }
        public DbSet<Worker> WorkerTable { get; set; }
        public DbSet<FishFarm> FishFarmTable { get; set; }
        public DbSet<Client> ClientTable { get; set; }
    }
}
