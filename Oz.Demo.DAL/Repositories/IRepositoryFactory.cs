using Oz.Demo.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oz.Demo.DAL.Repositories
{
    /// <summary>
    /// Фабрика создания репозиториев
    /// </summary>
	public interface IRepositoryFactory
	{
        /// <summary>
        /// Создание тестового репозитория
        /// </summary>
        /// <typeparam name="T">Тип Entity</typeparam>
        /// <typeparam name="M">Тип идентификатора entity</typeparam>
        /// <returns>Тестовый Репозиторий</returns>
       // ITestingRepository<T, M> GetTest<T, M>()
       // where T : class;

        /// <summary>
        /// Создание production репозитория
        /// </summary>
        /// <typeparam name="T">Тип Entity</typeparam>       
        /// <returns>Production Репозиторий</returns>
        IProductionRepository<T> Get<T>(string activeUser)
        where T : Record;


        /// <summary>
        /// Создание production кэшированного репозитория
        /// </summary>
        /// <typeparam name="T">Тип Entity</typeparam>
        /// <typeparam name="M">Тип идентификатора entity</typeparam>
        /// <returns>production кэшированный репозиторий</returns>
        //ICachedProductionRepository<T, M> GetCached<T, M>()
        //where T : class;

        /// <summary>
        /// Создание тестового кэшированного репозитория
        /// </summary>
        /// <typeparam name="T">Тип Entity</typeparam>
        /// <typeparam name="M">Тип идентификатора entity</typeparam>
        /// <returns>Тестовый кэшированный Репозиторий</returns>
        //ICachedTestRepository<T, M> GetCachedTest<T, M>()
        //where T : class;

    }
}
