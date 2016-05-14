using Microsoft.AspNet.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeMvc.Core.Routing
{
    public interface IRouteBuilder
    {
        IApplicationBuilder ApplicationBuilder { get; }
        IRouter DefaultHadler { get; set; }
        IServiceProvider ServiceProvider { get; }
        IList<IRouter> Routes { get; }
        IRouter Build();
    }
}
