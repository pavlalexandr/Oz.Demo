using Oz.Demo.BAL.Intrefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oz.Demo.BAL.Model;
using Oz.Demo.DAL.Repositories;
using Oz.Demo.DAL.Model;
using System.Data.Entity;

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
        public async Task CreateAsync(RegionModel model)
        {
            ThrowIfNotInit();
            await _regionRepo.SaveAsync(model.SetModel(null));
        }

        public async Task DeleteAsync(int id)
        {
            ThrowIfNotInit();
            var item = await _regionRepo.GetAsync(id);
            if (item != null)
            {
                await _regionRepo.DeleteAsync(item);
            }
        }

        public async Task<IEnumerable<RegionModel>> GetAsync(bool? nameSort = default(bool?), bool? timeZoneSort = default(bool?))
        {
            ThrowIfNotInit();
            var result = _regionRepo.All().Where(o=>!o.IsDeleted);
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
            var asyncResult = await result.ToListAsync();
            return asyncResult.Select(o => new RegionModel(o));
        }

        public async Task<RegionModel> GetByIdAsync(int id)
        {
            ThrowIfNotInit();
            var result = await _regionRepo.GetAsync(id);
            if (result != null && !result.IsDeleted)
                return new RegionModel(result);
            else return null;
        }

        public async Task UpdateAsync(RegionModel model)
        {
            ThrowIfNotInit();
            var toUpdate = await _regionRepo.GetAsync(model.Id);
            model.SetModel(toUpdate);
            await _regionRepo.SaveAsync(toUpdate);            
        }

        protected override void InternalInit(string activeUser)
        {
            _regionRepo = _repoFactory.Get<Region>(activeUser);
        }
    }
}
