using Oz.Demo.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oz.Demo.DAL.Repositories
{
    public interface IProductionRepository<T>:IRepository<T>
        where T : Record
    {
    }
}
