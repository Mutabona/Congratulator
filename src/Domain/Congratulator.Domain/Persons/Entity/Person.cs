using Congratulator.Domain.Base.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congratulator.Domain.Persons.Entity
{
    /// <summary>
    /// Человек.
    /// </summary>
    public class Person : BaseEntity
    {
        /// <summary>
        /// Имя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// День рождения.
        /// </summary>
        public DateOnly Birthday { get; set; }

        /// <summary>
        /// Фотография.
        /// </summary>
        public byte[] Photo { get; set; }
    }
}
