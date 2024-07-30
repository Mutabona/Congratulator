using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congratulator.Contracts.Persons
{
    /// <summary>
    /// Запрос на добавление человека.
    /// </summary>
    public class AddPersonRequest
    {
        /// <summary>
        /// Имя.
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        [Required]
        public string MiddleName { get; set; }

        /// <summary>
        /// День рождения.
        /// </summary>
        [Required]
        public DateOnly Birthday { get; set; }

        /// <summary>
        /// Фотография.
        /// </summary>
        [Required]
        public IFormFile Photo { get; set; }
    }
}
