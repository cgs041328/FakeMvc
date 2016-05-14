using FakeMvc.Core.Routing.Constraints;
using System;
using System.Collections.Generic;

namespace FakeMvc.Core.Routing
{
    internal class RouteConstraintBuilder
    {
        private readonly IInlineConstraintResolver _inlineConstraintResolver;
        private readonly string _displayName;

        private readonly Dictionary<string, List<IRouteConstraint>> _constraints;
        private readonly HashSet<string> _optionalParameters;
        public RouteConstraintBuilder(IInlineConstraintResolver inlineConstraintResolver, string displayName)
        {
            if (inlineConstraintResolver == null)
            {
                throw new ArgumentNullException(nameof(inlineConstraintResolver));
            }

            if (displayName == null)
            {
                throw new ArgumentNullException(nameof(displayName));
            }

            _inlineConstraintResolver = inlineConstraintResolver;
            _displayName = displayName;

            _constraints = new Dictionary<string, List<IRouteConstraint>>(StringComparer.OrdinalIgnoreCase);
            _optionalParameters = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }
        public IDictionary<string,IRouteConstraint> Build()
        {
            var constraints = new Dictionary<string, IRouteConstraint>(StringComparer.OrdinalIgnoreCase);
            foreach (var cons in _constraints)
            {
                IRouteConstraint constraint;
                if (cons.Value.Count==1)
                {
                    constraint = cons.Value[0];
                }
                else
                {
                    constraint = new CompositeRouteConstraint(cons.Value.ToArray());
                }
                if (_optionalParameters.Contains(cons.Key))
                {
                    var optionalConstraint = new OptionalRouteConstraint(constraint);
                    constraints.Add(cons.Key, optionalConstraint);
                }
                else
                {
                    constraints.Add(cons.Key, constraint);
                }
            }
            return constraints;

        }
        public void AddConstraints(string key, object value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            var constraint = value as IRouteConstraint;
            if (constraint == null)
            {
                var regexPattern = value as string;
                if (regexPattern == null)
                {
                    throw new InvalidOperationException();
                }
                var constraintsRegEx = $"^{regexPattern}$";
                constraint = new RegexRouteConstraint(constraintsRegEx);
            }
            Add(key, constraint);
        }
        public void AddResolvedConstraint(string key, string constraintText)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (constraintText == null)
            {
                throw new ArgumentNullException(nameof(constraintText));
            }

            var constraint = _inlineConstraintResolver.ResolveConstraint(constraintText);
            if (constraint == null)
            {
                throw new InvalidOperationException();
            }

            Add(key, constraint);
        }
        public void SetOptional(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            _optionalParameters.Add(key);
        }
        private void Add(string key, IRouteConstraint constraint)
        {
            List<IRouteConstraint> list;
            if (!_constraints.TryGetValue(key, out list))
            {
                list = new List<IRouteConstraint>();
                _constraints.Add(key, list);
            }
            list.Add(constraint);
        }
    }
}