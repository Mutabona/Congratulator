using Congratulator.Contracts.Persons;
using Congratulator.Domain.Persons.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congratulator.AppServices.Persons.Repositories
{
    /// <summary>
    /// Репозиторий для работы с людьми.
    /// </summary>
    public interface IPersonRepository
    {
        /// <summary>
        /// Возвращает всех людей.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список людей</returns>
        Task<IEnumerable<PersonDto>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает людей с ближайшими днями рождения.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список людей с ближайшими днями рождения.</returns>
        Task<IEnumerable<PersonDto>> GetWithNearestBirthdaysAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает людей по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Человек с заданным идентификатором.</returns>
        Task<PersonDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет человека.
        /// </summary>
        /// <param name="entity">Человек.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task AddAsync(AddPersonRequest entity, CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет человека.
        /// </summary>
        /// <param name="entity">Человек.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task UpdateAsync(PersonDto entity, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет человека по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
