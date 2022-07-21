using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace DS3.Native.Hoster.EntityFramework.Core;

[AppDbContext("DS3.Native.Hoster", DbProvider.Sqlite)]
public class DefaultDbContext : AppDbContext<DefaultDbContext>
{
    public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
    {
    }
}
