using Oz.Demo.BAL.Intrefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oz.Demo.BAL.Model;
using Oz.Demo.DAL.Repositories;
using Oz.Demo.DAL.Model;

namespace Oz.Demo.BAL.Services
{
    public class RegionService : Initializable, IRegionService
    {
        IRepositoryFactory _repoFactory;
        IRepository<Region> _regionRepo;
        public RegionService(IRepositoryFactory repoFactory)
        {
            _repoFactory = repoFactory;
        }
        public void Create(RegionModel model)
        {
            ThrowIfNotInit();
            _regionRepo.Save(model.SetModel(null));
        }

        public IEnumerable<RegionModel> Get(bool? nameSort = default(bool?), bool? timeZoneSort = default(bool?))
        {
            ThrowIfNotInit();
            var result = _regionRepo.All();
            if (nameSort.HasValue)
            {
                if (nameSort.Value)
                    result = result.OrderBy(o => o.Name);
                else result = result.OrderByDescending(o => o.Name);
            }
            if (timeZoneSort.HasValue)
            {
                if (timeZoneSort.Value)
                    result = result.OrderBy(o => o.TimeZone);
                else result = result.OrderByDescending(o => o.TimeZone);
            }
            return result.ToList().Select(o => new RegionModel(o));
        }

        public void Update(RegionModel model)
        {
            ThrowIfNotInit();
            var toUpdate = _regionRepo.Get(model.Id);
            model.SetModel(toUpdate);
            _regionRepo.Save(toUpdate);            
        }

        protected override void InternalInit(string activeUser)
        {
            _regionRepo = _repoFactory.Get<Region>(activeUser);
        }
    }
}
