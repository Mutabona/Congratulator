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
        Task<IEnumerable<PersonDto>> GetAllAsync(CancellationToken cancellationToken);

        Task<PersonDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task AddAsync(AddPersonRequest entity, CancellationToken cancellationToken);

        Task UpdateAsync(PersonDto entity, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
