using Congratulator.Domain.Persons.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congratulator.DataAccess.Configurations
{
    /// <summary>
    /// Файл конфигурации сущности человека.
    /// </summary>
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder
                .ToTable("Persons")
                .HasKey(e => e.Id);

            builder
                .Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(e => e.MiddleName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(e => e.Birthday)
                .IsRequired();
        }
    }
}
