using AutoMapper;
using Congratulator.ComponentRegistrar.Mappers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congratulator.ComponentRegistrar
{
    /// <summary>
    /// Регистратор компонентов.
    /// </summary>
    public static class Registrar
    {
        /// <summary>
        /// Добавляет сервисы в коллекцию.
        /// </summary>
        /// <param name="services">Коллекция сервисов <see cref="IServiceCollection"/>.</param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.ConfigureAutomapper();
        }

        /// <summary>
        /// Добавляет автомаппер в сервисы.
        /// </summary>
        /// <param name="services">Коллекция сервисов <see cref="IServiceCollection"/>.</param>
        /// <returns></returns>
        private static IServiceCollection ConfigureAutomapper(this IServiceCollection services)
        {
            return services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));
        }

        /// <summary>
        /// Возвращает конфигурацию автомаппера.
        /// </summary>
        /// <returns>Конфигурация автомаппера <see cref="MapperConfiguration"/>.</returns>
        private static MapperConfiguration GetMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PersonProfile>();
            });
            config.AssertConfigurationIsValid();
            return config;
        }
    }
}
