using Microsoft.AspNet.Http;

namespace FakeMvc.Core.Routing.Constraints
{
    public interface IRouteConstraint
    {
        bool Match(
    HttpContext httpContext,
    IRouter route,
    string routeKey,
    RouteValueDictionary values,
    RouteDirection routeDirection);
    }
}
