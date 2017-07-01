using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oz.Demo.DAL.Model;

namespace Oz.Demo.DAL.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public IProductionRepository<T> Get<T>(string activeUser) where T : Record
        {
            return (IProductionRepository<T>)Activator.CreateInstance(typeof(ProductionRepository<T>), activeUser);
        }
    }
}
