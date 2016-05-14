using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeMvc.Core.Routing
{
    public class RouteHandler : IRouteHandler, IRouter
    {
        private readonly RequestDelegate _requestDelegate;

        public RouteHandler(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public RequestDelegate GetRequestHandler(HttpContext httpContext, RouteData routeData)
        {
            return _requestDelegate;
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            // Nothing to do.
            return null;
        }

        public Task RouteAsync(RouteContext context)
        {
            context.Handler = _requestDelegate;
            return Task.FromResult(0);
        }
    }
}
