using Congratulator.AppServices.Persons.Services;
using Congratulator.Contracts.Persons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Congratulator.API.Controllers
{
    /// <summary>
    /// Контроллер для работы с людьми.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        /// <summary>
        /// Создаёт экземпляр <see cref="PersonController"/>
        /// </summary>
        /// <param name="personService">Сервис для работы с людьми</param>
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// Возвращает всех людей.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список людей.</returns>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(IEnumerable<PersonDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _personService.GetPersonsAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Возвращает фото человека по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор человека.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Фото человека.</returns>
        [HttpGet]
        [Route("{id}/photo")]
        public async Task<IActionResult> GetUserPhoto([FromRoute]Guid id, CancellationToken cancellationToken)
        {
            var result = await _personService.GetByIdAsync(id, cancellationToken);

            using var ms = new MemoryStream();
            await result.Photo.CopyToAsync(ms);

            var photoContent = ms.ToArray();
            var photoContentType = result.Photo.ContentType;

            return File(photoContent, photoContentType);
        }

        /// <summary>
        /// Возвращает людей с ближайшими днями рождения.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Список людей.</returns>
        [HttpGet]
        [Route("nearest")]
        [ProducesResponseType(typeof(IEnumerable<PersonDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPersonsWithNearestBirthdays(CancellationToken cancellationToken)
        {
            var result = await _personService.GetPersonsWithNearestBirthdaysAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Создаёт человека.
        /// </summary>
        /// <param name="request">Запрос на создание человека.<see cref="AddPersonRequest"/></param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddPerson([FromForm]AddPersonRequest request, CancellationToken cancellationToken)
        {
            if (request.Photo == null) return BadRequest();
            await _personService.AddAsync(request, cancellationToken);
            return Ok();
        }

        /// <summary>
        /// Удаляет человека по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeletePerson(Guid id, CancellationToken cancellationToken)
        {
            await _personService.DeleteAsync(id, cancellationToken);
            return Ok();
        }


        /// <summary>
        /// Обновляет человека.
        /// </summary>
        /// <param name="person">Человек <see cref="PersonDto"/>.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdatePerson([FromForm]PersonDto person, CancellationToken cancellationToken)
        {
            await _personService.UpdateAsync(person, cancellationToken);
            return Ok();
        }
    }
}
