using Congratulator.Contracts.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congratulator.AppServices.Persons.Services
{
    public interface IPersonService
    {
        public Task<IEnumerable<PersonDto>> GetPersonsAsync(CancellationToken cancellationToken);

        public Task AddAsync(AddPersonRequest request, CancellationToken cancellationToken);

        public Task UpdateAsync(PersonDto personDto, CancellationToken cancellationToken);

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
