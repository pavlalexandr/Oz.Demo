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
        IEnumerable<RegionModel> Get(bool? nameSort = null, bool? timeZoneSort = null);
        void Create(RegionModel model);
        void Update(RegionModel model);
    }
}
