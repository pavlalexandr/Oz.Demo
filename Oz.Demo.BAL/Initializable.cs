using Oz.Demo.BAL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oz.Demo.BAL
{
    public abstract class Initializable : IInitializable
    {
        protected bool _initialized;
        public bool Initialized => _initialized;
        protected abstract void InternalInit(string activeUser);
        public void Init(string activeUser)
        {
            InternalInit(activeUser);
            _initialized = true;
        }
        public void ThrowIfNotInit()
        {
            if (!Initialized) throw new NotInitializedException();
        }
        public static void InitAll(string activeUser, params IInitializable[] servicesToInit)
        {
            foreach (var service in servicesToInit)
            {
                if (!service.Initialized)
                    service.Init(activeUser);
            }
        }
    }
}
