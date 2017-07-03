using Oz.Demo.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Oz.Demo.DAL.Repositories
{
    /// <summary>
    /// Общий интерфейс репозитория
    /// </summary>
    public interface IRepository { }
    /// <summary>
    /// Общий интерфейс репозитория на основе Тип данных и Типа данных идентификатора
    /// </summary>
    /// <typeparam name="T">Тип данных репозитория</typeparam>    
	public interface IRepository<T> : IRepository
        where T : Record
    {
        /// <summary>
        /// Является ли репозиторий тестовым
        /// </summary>
        //bool IsTest { get;  }
        /// <summary>
        /// Выполнение запроса
        /// </summary>
        /// <param name="action">условие</param>
        /// <returns></returns>
        IQueryable<T> Query(Expression<Func<T, bool>> action);
        /// <summary>
        /// Полечение всех записей
        /// </summary>
        /// <returns></returns>
        IQueryable<T> All();
        /// <summary>
        /// Получение записи по идентификатору
        /// </summary>
        /// <param name="id">Значение Идентификатора</param>        
        /// <returns></returns>
        T Get(int id);
        Task<T> GetAsync(int id);
        /// <summary>
        /// Созранение записи
        /// </summary>
        /// <param name="itemToSave">Entity для сохранения</param>        
        void Save(T itemToSave);
        Task SaveAsync(T itemToSave);
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="itemToSave">Entity для сохранения</param>
        void Delete(T itemToSave);
        Task DeleteAsync(T itemToSave);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        //void BulkInsert(IEnumerable<T> entities);
        /// <summary>
        /// Добавление 
        /// </summary>
        /// <param name="entity"></param>
        //void Insert(T entity);
    }
}
