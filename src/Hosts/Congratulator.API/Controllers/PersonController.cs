using Congratulator.AppServices.Persons.Services;
using Congratulator.Contracts.Persons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Congratulator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _personService.GetPersonsAsync(cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddPerson(AddPersonRequest request, CancellationToken cancellationToken)
        {
            await _personService.AddAsync(request, cancellationToken);
            return Ok();
        }
    }
}
