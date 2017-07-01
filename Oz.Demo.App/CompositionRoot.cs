using LightInject;
using Oz.Demo.BAL.Intrefaces;
using Oz.Demo.BAL.Services;
using Oz.Demo.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OzDemoApp
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IRepositoryFactory, RepositoryFactory>(new PerRequestLifeTime());
            serviceRegistry.Register<IRegionService, RegionService>(new PerRequestLifeTime());
        }
    }
}
