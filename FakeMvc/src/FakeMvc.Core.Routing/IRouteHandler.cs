using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeMvc.Core.Routing
{
    public interface IRouteHandler
    {
        RequestDelegate GetRequestHandler(HttpContext httpContext, RouteData routeData);
    }
}
