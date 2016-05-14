using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeMvc.Core.Routing
{
    public class RouteMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRouter _router;
        public RouteMiddleware(RequestDelegate next,IRouter router)
        {
            _next = next;
            _router = router;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            var routeContext = new RouteContext(httpContext);
            routeContext.RouteData.Routers.Add(_router);
            await _router.RouteAsync(routeContext);
            if(routeContext.Handler == null)
            {
                await _next.Invoke(httpContext);
            }
            else
            {
                await routeContext.Handler(routeContext.HttpContext);
            }
        }
    }
}
