using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeMvc.Core.Routing
{
    public interface IRouteCollection : IRouter
    {
        void Add(IRouter router);
    }
}
