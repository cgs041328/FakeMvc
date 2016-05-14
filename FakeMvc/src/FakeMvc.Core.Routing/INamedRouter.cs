namespace FakeMvc.Core.Routing
{
    public interface INamedRouter:IRouter
    {
         string Name { get;}
    }
}