using DATA.Entities;
using Microsoft.EntityFrameworkCore;


namespace DATA.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Note> Notes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
            .Property(p => p.Version)
            .IsRowVersion()
            .ValueGeneratedOnAddOrUpdate()
            .IsConcurrencyToken();

            modelBuilder.Entity<Note>()
           .Property(p => p.Version)
           .IsRowVersion()
           .ValueGeneratedOnAddOrUpdate()
           .IsConcurrencyToken();
        }
    }
}
