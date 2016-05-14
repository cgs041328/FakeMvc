using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeMvc.Core.Routing.Template;
using FakeMvc.Core.Routing.Constraints;

namespace FakeMvc.Core.Routing
{
    public abstract class RouteBase:IRouter
    {
        private TemplateMatcher _matcher;
        private TemplateBinder _binder;
        private readonly IReadOnlyDictionary<string,IRouteConstraint> _constraints;
        private readonly RouteValueDictionary _dataTokens;
        private readonly RouteValueDictionary _defaults;
        private IRouter _target;
        private RouteTemplate _parsedTemplate;
        private string _template;
        public virtual IDictionary<string, IRouteConstraint> Constraints { get; protected set; }

        protected virtual IInlineConstraintResolver ConstraintResolver { get; set; }

        public virtual RouteValueDictionary DataTokens { get; protected set; }

        public virtual RouteValueDictionary Defaults { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual RouteTemplate ParsedTemplate { get; protected set; }

        protected abstract Task OnRouteMatched(RouteContext context);

        protected abstract VirtualPathData OnVirtualPathGenerated(VirtualPathContext context);
        //private ILogger _logger;
        public RouteBase(
            string template,
            string name,
            IInlineConstraintResolver constraintResolver,
            RouteValueDictionary defaults,
            IDictionary<string,object> constraints,
            RouteValueDictionary dataTokens
            )
        {
            if(constraintResolver == null)
            {
                throw new ArgumentNullException(nameof(constraintResolver));
            }
            template = template ?? string.Empty;
            Name = name;
            ConstraintResolver = constraintResolver;
            DataTokens = dataTokens ?? new RouteValueDictionary();
            ParsedTemplate = TemplateParser.Parse(template);
            Constraints = GetConstraints(constraintResolver, ParsedTemplate, constraints);
        }
        protected static IDictionary<string,IRouteConstraint> GetConstraints(
            IInlineConstraintResolver inlineConstraintResolver,
            RouteTemplate parsedTemplate,
            IDictionary<string,object> constraints
            )
        {
            var constraintBuilder = new RouteConstraintBuilder(inlineConstraintResolver, parsedTemplate.TemplateText);
            if (constraints != null)
            {
                foreach(var cons in constraints)
                {
                    constraintBuilder.AddConstraints(cons.Key, cons.Value);
                }
            }
            foreach (var parameter in parsedTemplate.Parameters)
            {
                if (parameter.IsOptional)
                {
                    constraintBuilder.SetOptional(parameter.Name);
                }

                foreach (var inlineConstraint in parameter.InlineConstraints)
                {
                    constraintBuilder.AddResolvedConstraint(parameter.Name, inlineConstraint.Constraint);
                }
            }

            return constraintBuilder.Build();
        }

        public Task RouteAsync(RouteContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            EnsureMatcher();
            var requestPath = context.HttpContext.Request.Path;
            if (!_matcher.TryMatch(requestPath, context.RouteData.Values))
            {
                return Task.FromResult(0);
            }
            if (DataTokens.Count > 0)
            {
                MergeValues(context.RouteData.DataTokens, DataTokens);
            }
            return OnRouteMatched(context);
        }
private void MergeValues(RouteValueDictionary dataTokens1, RouteValueDictionary dataTokens2)
        {
            throw new NotImplementedException();
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            throw new NotImplementedException();
        }
        private void EnsureMatcher()
        {
            if(_matcher == null)
            {
                _matcher = new TemplateMatcher(ParsedTemplate, Defaults);
            }
        }
        public override string ToString()
        {
            return ParsedTemplate.TemplateText;
        }
    }
}
