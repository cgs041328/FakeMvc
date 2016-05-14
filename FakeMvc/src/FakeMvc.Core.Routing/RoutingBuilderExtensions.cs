using Microsoft.AspNet.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeMvc.Core.Routing
{
    public static class RoutingBuilderExtensions
    {
        public static IApplicationBuilder UseRouter(this IApplicationBuilder app,IRouter router)
        {
           return app.UseMiddleware<RouteMiddleware>(router);
        }
    }
}
