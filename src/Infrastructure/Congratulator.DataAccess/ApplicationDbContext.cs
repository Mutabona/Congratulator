using Congratulator.DataAccess.Configurations;
using Congratulator.Domain.Persons.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Congratulator.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
        }
    }
}
