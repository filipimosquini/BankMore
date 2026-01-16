using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq;

namespace Authentication.Infrastructure.Contexts;

public class IdentityContext : IdentityDbContext
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }
    public bool MigrateDatabase()
    {
        var idsDasMigrationJaExecutadas = this.GetService<IHistoryRepository>()
            .GetAppliedMigrations()
            .Select(m => m.MigrationId);

        var idsDeTodasAsMigrations = this.GetService<IMigrationsAssembly>()
            .Migrations
            .Select(m => m.Key);

        return !idsDeTodasAsMigrations.Except(idsDasMigrationJaExecutadas).Any();
    }
}