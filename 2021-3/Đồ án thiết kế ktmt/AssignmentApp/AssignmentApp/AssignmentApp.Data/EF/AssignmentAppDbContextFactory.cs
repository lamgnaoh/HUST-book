using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AssignmentApp.Data.EF;

public class AssignmentAppDbContextFactory: IDesignTimeDbContextFactory<AssignmentAppDbContext>
{
    public AssignmentAppDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();
        var connectionString = configuration.GetConnectionString("AssignmentAppDatabase");
        var optionsBuilder = new DbContextOptionsBuilder<AssignmentAppDbContext>();
        optionsBuilder.UseSqlServer(connectionString);
        return new AssignmentAppDbContext(optionsBuilder.Options);

    }
}