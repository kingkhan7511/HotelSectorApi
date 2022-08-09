using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration; 
using HotelSector.Core.EntityFrameworkCore.Contexts; 
using System.IO;

namespace Cms.Fml.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class HotelSectorDbContextFactory : IDesignTimeDbContextFactory<HotelSectorDbContext>
    { 

        public HotelSectorDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../HotelSector.Core/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<HotelSectorDbContext>();
            var connectionString = configuration.GetConnectionString("Default");
            builder.UseSqlServer(connectionString);
            return new HotelSectorDbContext(builder.Options);
        }
    }
}
