namespace sakan.Models
{
    public class SakanDbContext : DbContext
    {
        public SakanDbContext(DbContextOptions<SakanDbContext> options) : base(options)
        {
        }

        public DbSet<House> Houses { get; set; }
        public DbSet<HouseOwner> HouseOwners { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<AdminHouse> AdminHouses { get; set; }


        
        protected override void OnModelCreating(ModelBuilder x)
        {
         x.Entity<Student>()
         .HasIndex(u => u.Email)
         .IsUnique();


            x.Entity<HouseOwner>()
        .HasIndex(u => u.Email)
        .IsUnique();

        }
    }
}
