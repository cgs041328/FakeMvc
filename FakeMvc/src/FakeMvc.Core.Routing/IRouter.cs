using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeMvc.Core.Routing
{
    public interface IRouter
    {
        Task RouteAsync(RouteContext context);
        VirtualPathData GetVirtualPath(VirtualPathContext context);
    }
}
