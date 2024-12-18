using Microsoft.EntityFrameworkCore;

namespace Lab2.DataAccess
{
    public class HouseDbContext : DbContext
    {
        public DbSet<House> Houses { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Garage> Garages { get; set; }
        public HouseDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Git_hub\LB2_DataAccess\Lab2.DataAccess\HouseDb.mdf;Integrated Security=True");
            }
        }

    }
}
