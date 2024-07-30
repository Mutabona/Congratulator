using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Congratulator.AppServices.Persons.Repositories;
using Congratulator.Contracts.Persons;
using Congratulator.Domain.Persons.Entity;
using Congratulator.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Congratulator.DataAccess.Persons.Repository
{
    ///<inheritdoc cref="IPersonRepository"/>
    public class PersonRepository : IPersonRepository
    {
        private readonly IRepository<Person> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Создаёт экземпляр <see cref="PersonRepository"/>.
        /// </summary>
        /// <param name="repository">Репозиторий <see cref="IRepository{TEntity}"/>.</param>
        /// <param name="mapper">Автомаппер <see cref="IMapper"/>.</param>
        public PersonRepository(IRepository<Person> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        ///<inheritdoc/>
        public async Task AddAsync(AddPersonRequest entity, CancellationToken cancellationToken)
        {
            var person = _mapper.Map<Person>(entity);
            await _repository.AddAsync(person, cancellationToken);
        }

        ///<inheritdoc/>
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(id, cancellationToken);
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<PersonDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _repository.GetAll().ProjectTo<PersonDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }

        ///<inheritdoc/>
        public async Task<PersonDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var person = await _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return person;
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<PersonDto>> GetWithNearestBirthdaysAsync(CancellationToken cancellationToken)
        {
            var date = DateOnly.FromDateTime(DateTime.Today);
            var persons = await _repository.GetAll()
                .Where(s => s.Birthday.AddYears(date.Year - s.Birthday.Year).DayOfYear - date.DayOfYear <= 7 && s.Birthday.AddYears(date.Year - s.Birthday.Year).DayOfYear - date.DayOfYear >= 0)
                .ProjectTo<PersonDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return persons;
        }

        ///<inheritdoc/>
        public async Task UpdateAsync(PersonDto entity, CancellationToken cancellationToken)
        {
            var person = _mapper.Map<Person>(entity);
            await _repository.UpdateAsync(person, cancellationToken);
        }
    }
}
