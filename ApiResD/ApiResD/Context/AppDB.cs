using ApiResD.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiResD.Context
{
    public class AppDB : DbContext
    {
        public AppDB(DbContextOptions<AppDB> options): base(options)
        { }
        public DbSet<Person> People { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Pedido> Order { get; set; }
        public DbSet<Users> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var passwprd = PasswordHasher.HashPassword("admin");
            _ = modelBuilder.Entity<Users>().HasData([

                new Users
                {
                    id = 1,
                    userName = "admin",
                    password = passwprd
                },
                ]);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // Suprimir la advertencia específica
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

    }
}
