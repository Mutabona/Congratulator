using Microsoft.EntityFrameworkCore;
using Congratulator.DataAccess;

public class MigrationDbContext : ApplicationDbContext
{
    public MigrationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }
}