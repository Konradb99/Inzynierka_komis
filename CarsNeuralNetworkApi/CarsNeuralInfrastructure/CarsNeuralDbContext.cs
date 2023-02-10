using CarsNeuralInfrastructure.Entities;
using CarsNeuralInfrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CarsNeuralInfrastructure
{
    public class CarsNeuralDbContext : DbContext
    {
        public CarsNeuralDbContext(DbContextOptions<CarsNeuralDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CarTrain> TrainSet { get; set; }
        public DbSet<CarTest> TestSet { get; set; }

        public new async Task SaveChanges()
        {
            await SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}