using Furion;
using Microsoft.Extensions.DependencyInjection;

namespace DS3.Native.Hoster.EntityFramework.Core;

public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDatabaseAccessor(options =>
        {
            options.AddDbPool<DefaultDbContext>();
        }, "DS3.Native.Hoster.Database.Migrations");
    }
}
