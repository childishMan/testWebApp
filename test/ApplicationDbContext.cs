using Microsoft.EntityFrameworkCore;
using test.DbModels;

namespace test
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Incident> Incidents { get; set; }

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasAlternateKey(c => c.Mail);
            modelBuilder.Entity<Account>(e =>
            {
                e.HasMany(a => a.Contacts);
                e.HasAlternateKey(a => a.Name);
            });
            modelBuilder.Entity<Incident>(e =>
            {
                e.Property(a => a.Name).ValueGeneratedOnAdd();
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder = optionsBuilder.UseSqlServer(
                @"Data Source=DESKTOP-5H9GP9D;Initial Catalog=TestDB;Integrated Security=True;MultipleActiveResultSets=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
