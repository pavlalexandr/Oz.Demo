using Oz.Demo.BAL.Model;
using Oz.Demo.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oz.Demo.BAL.Intrefaces
{
    public interface IRegionService: IInitializable
    {
        Task<IEnumerable<RegionModel>> GetAsync(bool? nameSort = null, bool? timeZoneSort = null);
        Task CreateAsync(RegionModel model);
        Task UpdateAsync(RegionModel model);
        Task DeleteAsync(int id);
        Task<RegionModel> GetByIdAsync(int id);
    }
}
