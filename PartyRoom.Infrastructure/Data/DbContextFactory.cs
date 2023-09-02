using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PartyRoom.Infrastructure.Data
{
	public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
		public DbContextFactory()
		{
		}

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var parent = Directory.GetParent(path: Directory.GetCurrentDirectory())
                                      .ToString();
            var basePath = Path.Combine(parent, "PartyRoom.WebAPI");

            var configuration = new ConfigurationBuilder()
                                .SetBasePath(basePath!)
                                .AddJsonFile("appsettings.json", optional: false)
                                .Build();


            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var dbContextBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString);

            return new ApplicationDbContext(dbContextBuilder.Options);
        }
    }
}

