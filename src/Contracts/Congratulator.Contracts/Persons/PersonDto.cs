using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congratulator.Contracts.Persons
{
    /// <summary>
    /// Человек.
    /// </summary>
    public class PersonDto
    {
        /// <summary>
        /// Идентификатор записи.
        /// </summary>
        public Guid Id { get; set; }

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
    }
}
