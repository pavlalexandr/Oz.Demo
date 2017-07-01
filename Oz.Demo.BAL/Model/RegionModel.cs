using Oz.Demo.DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oz.Demo.BAL.Model
{
    public class RegionModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(10)]
        public string TimeZone { get; set; }

        public RegionModel()
        {

        }
        public RegionModel(Region model)
        {
            Id = model.ID;
            Name = model.Name;
            TimeZone = model.TimeZone;
        }

        public Region SetModel(Region model)
        {
            if (model == null) model = new Region();
            model.ID = Id;
            model.Name = Name;
            model.TimeZone = TimeZone;
            return model;
        }
    }
}
