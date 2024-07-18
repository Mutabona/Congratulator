using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congratulator.Domain.Base.Entity
{
    /// <summary>
    /// Бзовый класс для всех сущностей.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Идентификатор записи.
        /// </summary>
        public Guid Id { get; set; }
    }
}