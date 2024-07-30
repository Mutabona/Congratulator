using Congratulator.Contracts.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congratulator.AppServices.Persons.Services
{
    /// <summary>
    /// Сервис для работы с людьми.
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// Возвращает всех людей.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список людей <see cref="IEnumerable{PersonDto}"/></returns>
        public Task<IEnumerable<PersonDto>> GetPersonsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает людей с ближайшими днями рождения.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список людей с ближайшими днями рождения <see cref="IEnumerable{PersonDto}"/>.</returns>
        public Task<IEnumerable<PersonDto>> GetPersonsWithNearestBirthdaysAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает людей по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Человек с заданным идентификатором.</returns>
        public Task<PersonDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет человека.
        /// </summary>
        /// <param name="entity">Человек.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        public Task AddAsync(AddPersonRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Обновляет человека.
        /// </summary>
        /// <param name="entity">Человек.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        public Task UpdateAsync(PersonDto personDto, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет человека по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
