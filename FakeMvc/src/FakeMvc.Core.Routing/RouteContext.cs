using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;

namespace FakeMvc.Core.Routing
{
    public class RouteContext
    {
        private RouteData _routeData;
        public HttpContext HttpContext { get; }
        public RequestDelegate Handler { get; set; }
        public RouteData RouteData
        {
            get
            {
                return _routeData;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(RouteData));
                }

                _routeData = value;
            }
        }
        public RouteContext(HttpContext httpContext)
        {
            HttpContext = httpContext;

            RouteData = new RouteData();
        }
    }
}