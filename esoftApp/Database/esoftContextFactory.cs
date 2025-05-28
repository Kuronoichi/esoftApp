using Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class esoftContextFactory : IDesignTimeDbContextFactory<esoftContext>
{
    public esoftContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<esoftContext>();
        optionsBuilder.UseSqlServer(AppConfig.GetConnectionString("ObjectivePlatformConnection"));

        return new esoftContext(optionsBuilder.Options);
    }
}