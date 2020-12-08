
using System.Linq;
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /* 
    StoreContext extends  DbContext which is a session with the database that can be
    used to query and save instances of entities. DbContext is a cominbation
    of the Unit of Work and Repository Pattern.
    */
    public class StoreContext : DbContext
    {
        //options is used to configure the StoreContext and is based to base DbContext constructor
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        //Products, ProductBrands and ProductTypes are generated tables
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        //Used to manually create a migration to configure an entity
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Call onModelCreating in base class DbContext which applies configuration 
            //from our ProductConfiguration class that implements IEntityTypeConfiguration<TEntity

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(
                        p => p.PropertyType == typeof(decimal)
                    );

                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }
                }
            }
        }
    }
}