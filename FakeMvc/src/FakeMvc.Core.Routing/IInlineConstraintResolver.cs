using FakeMvc.Core.Routing.Constraints;

namespace FakeMvc.Core.Routing
{
    public interface IInlineConstraintResolver
    {
        IRouteConstraint ResolveConstraint(string inlineConstraint);
    }
}