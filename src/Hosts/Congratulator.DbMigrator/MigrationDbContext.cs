using Microsoft.EntityFrameworkCore;
using Congratulator.DataAccess;

/// <summary>
/// Контекст миграции базы данных.
/// </summary>
public class MigrationDbContext : ApplicationDbContext
{
    /// <summary>
    /// Создаёт экземпляр <see cref="MigrationDbContext"/>
    /// </summary>
    /// <param name="dbContextOptions"></param>
    public MigrationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }
}