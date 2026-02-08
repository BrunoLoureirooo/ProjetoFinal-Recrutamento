using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using PRH.Server.Repository;
using Microsoft.EntityFrameworkCore;

namespace PRH.SERVER.API.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            //var builder = new DbContextOptionsBuilder<RepositoryContext>()
            //    .UseSqlServer(configuration.GetConnectionString("sqlConnection"));

            var builder = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlServer(configuration.GetConnectionString("sqlConnection"),
                    b => b.MigrationsAssembly("PRH.Server.API"));


            return new RepositoryContext(builder.Options);
        }
    }
}
