using Congratulator.AppServices.Persons.Repositories;
using Congratulator.Contracts.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congratulator.AppServices.Persons.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(AddPersonRequest request, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(request, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<PersonDto>> GetPersonsAsync(CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(cancellationToken);
        }

        public async Task UpdateAsync(PersonDto personDto, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(personDto, cancellationToken);
        }
    }
}
