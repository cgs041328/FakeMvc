using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeMvc.Core.Routing
{
    public class Route : RouteBase
    {
        private readonly IRouter _target;
        public string RouteTemplate => ParsedTemplate.TemplateText;
        public Route(IRouter target,string routeTemplate,IInlineConstraintResolver inlineConstraintResolver)
                        : this(
                target,
                routeTemplate,
                defaults: null,
                constraints: null,
                dataTokens: null,
                inlineConstraintResolver: inlineConstraintResolver)
        {

        }
        public Route(IRouter target, string routeTemplate, RouteValueDictionary defaults, IDictionary<string, object> constraints, RouteValueDictionary dataTokens, IInlineConstraintResolver inlineConstraintResolver)
            :this(target, null, routeTemplate, defaults, constraints, dataTokens, inlineConstraintResolver)
        {

        }
        public Route(
    IRouter target,
    string routeName,
    string routeTemplate,
    RouteValueDictionary defaults,
    IDictionary<string, object> constraints,
    RouteValueDictionary dataTokens,
    IInlineConstraintResolver inlineConstraintResolver)
    : base(
          routeTemplate,
          routeName,
          inlineConstraintResolver,
          defaults,
          constraints,
          dataTokens)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            _target = target;
        }
        protected override Task OnRouteMatched(RouteContext context)
        {
            context.RouteData.Routers.Add(_target);
            return _target.RouteAsync(context);
        }

        protected override VirtualPathData OnVirtualPathGenerated(VirtualPathContext context)
        {
            return _target.GetVirtualPath(context);
        }
    }
}
