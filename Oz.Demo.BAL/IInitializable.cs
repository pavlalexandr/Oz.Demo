using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oz.Demo.BAL
{
    public interface IInitializable
    {
        bool Initialized { get; }
        void Init(string activeUser);        
    }
}
