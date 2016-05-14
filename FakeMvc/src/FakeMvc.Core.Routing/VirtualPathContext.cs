using Microsoft.AspNet.Http;

namespace FakeMvc.Core.Routing
{
    public class VirtualPathContext
    {
        public HttpContext HttpContext { get; internal set; }
        public string RouteName { get; internal set; }
    }
}