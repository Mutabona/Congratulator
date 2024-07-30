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
    /// <summary>
    /// Контекст базы данных.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Набор людей.
        /// </summary>
        public DbSet<Person> Persons { get; set; }

        /// <summary>
        /// Создаёт экземпляр <see cref="ApplicationDbContext"/>
        /// </summary>
        /// <param name="dbContextOptions">Настройки базы данных.</param>
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        /// <summary>
        /// Применяет конфигурации.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
        }
    }
}
