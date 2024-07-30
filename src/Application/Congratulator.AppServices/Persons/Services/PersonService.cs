using Congratulator.AppServices.Persons.Repositories;
using Congratulator.Contracts.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congratulator.AppServices.Persons.Services
{
    ///<inheritdoc cref="IPersonService"/>
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

        /// <summary>
        /// Возвращает экземпляр <see cref="PersonService"/>
        /// </summary>
        /// <param name="repository">Репозиторий для работы с людьми.</param>
        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        ///<inheritdoc/>
        public async Task AddAsync(AddPersonRequest request, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(request, cancellationToken);
        }

        ///<inheritdoc/>
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(id, cancellationToken);
        }

        ///<inheritdoc/>
        public async Task<PersonDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<PersonDto>> GetPersonsAsync(CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<PersonDto>> GetPersonsWithNearestBirthdaysAsync(CancellationToken cancellationToken)
        {
            return await _repository.GetWithNearestBirthdaysAsync(cancellationToken);
        }

        ///<inheritdoc/>
        public async Task UpdateAsync(PersonDto personDto, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(personDto, cancellationToken);
        }
    }
}
