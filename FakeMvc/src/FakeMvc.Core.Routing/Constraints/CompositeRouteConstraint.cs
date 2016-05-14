using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace FakeMvc.Core.Routing.Constraints
{
    public class CompositeRouteConstraint : IRouteConstraint
    {
        public IEnumerable<IRouteConstraint> Constraints { get; private set; }
        public CompositeRouteConstraint(IEnumerable<IRouteConstraint> constraints)
        {
            if (constraints == null)
            {
                throw new ArgumentNullException(nameof(constraints));
            }

            Constraints = constraints;
        }

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            if (route == null)
            {
                throw new ArgumentNullException(nameof(route));
            }

            if (routeKey == null)
            {
                throw new ArgumentNullException(nameof(routeKey));
            }

            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }
            foreach (var constraint in Constraints)
            {
                if (!constraint.Match(httpContext, route, routeKey, values, routeDirection))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
