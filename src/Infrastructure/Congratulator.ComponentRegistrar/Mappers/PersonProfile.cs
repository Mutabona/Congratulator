using AutoMapper;
using Congratulator.Contracts.Persons;
using Congratulator.Domain.Persons.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congratulator.ComponentRegistrar.Mappers
{
    /// <summary>
    /// Профиль для работы с людьми.
    /// </summary>
    public class PersonProfile : Profile
    {
        public PersonProfile() 
        {
            CreateMap<AddPersonRequest, Person>()
                .ForMember(s => s.Id, map => map.MapFrom(s => Guid.NewGuid()));

            CreateMap<PersonDto, Person>();

            CreateMap<Person, PersonDto>();
        }
    }
}
