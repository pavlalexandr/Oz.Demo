using Microsoft.AspNetCore.Mvc;
using Oz.Demo.BAL.Intrefaces;
using Oz.Demo.BAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oz.Demo.App.Controllers
{
    [Route("api/[controller]")]
    public class RegionController:Controller
    {
        IRegionService _regionService;
        public RegionController(IRegionService regionService)
        {
            _regionService = regionService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(bool? nameSort, bool? timeZoneSort)
        {
            if (!_regionService.Initialized)
                _regionService.Init(User.Identity.Name);
            return Ok((await _regionService.GetAsync(nameSort, timeZoneSort)));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!_regionService.Initialized)
                _regionService.Init(User.Identity.Name);
            return Ok((await _regionService.GetByIdAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegionModel model)
        {            
            if (ModelState.IsValid)
            {
                if (!_regionService.Initialized)
                    _regionService.Init(User.Identity.Name);
                if (model.Id == 0)
                {
                    await _regionService.CreateAsync(model);
                }
                else
                {
                    await _regionService.UpdateAsync(model);
                }
                return Ok();
            }
            else
                return BadRequest(ModelState.ValidationState);
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!_regionService.Initialized)
                _regionService.Init(User.Identity.Name);
            await _regionService.DeleteAsync(id);
            return Ok();
        }
    }
}
