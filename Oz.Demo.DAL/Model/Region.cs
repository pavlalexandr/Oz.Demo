using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oz.Demo.DAL.Model
{
    public class Region : Record
    {
        public string Name { get; set; }
        public string TimeZone { get; set; }
    }
}
