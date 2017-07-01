using Oz.Demo.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oz.Demo.DAL.Repositories
{
    /// <summary>
    /// Боевой репозиторий
    /// </summary>
    /// <typeparam name="T">Тип данных репозитория</typeparam>
    /// <typeparam name="M">Типа данных идентификатора</typeparam>
    public class ProductionRepository<T>:Repository<T>,IProductionRepository<T>
        where T : Record
    {
        public ProductionRepository(string activeUser) : base(activeUser)
        {

        }
    }
}
