using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using System.Globalization;

namespace FakeMvc.Core.Routing.Constraints
{
    public class RegexRouteConstraint:IRouteConstraint
    {
        private static readonly TimeSpan RegexMatchTimeout = TimeSpan.FromSeconds(10);
        public Regex Constraint { get; private set; }
        public RegexRouteConstraint(Regex regex)
        {
            if (regex == null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            Constraint = regex;
        }
        public RegexRouteConstraint(string regexPattern)
        {
            if (regexPattern == null)
            {
                throw new ArgumentNullException(nameof(regexPattern));
            }

            Constraint = new Regex(
                regexPattern,
                RegexOptions.CultureInvariant | RegexOptions.IgnoreCase,
                RegexMatchTimeout);
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
            object routeValue;
            if (values.TryGetValue(routeKey, out routeValue)  && routeValue != null)
            {
                var parameterValueString = Convert.ToString(routeValue, CultureInfo.InvariantCulture);

                return Constraint.IsMatch(parameterValueString);
            }

            return false;
        }
    }
}
