using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oz.Demo.DAL.Model
{
    public class OzDemoDbContext:DbContext
    {
        public DbSet<Region> Regions { get; set; }
    }
}
