using Microsoft.AspNet.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeMvc.Core.Routing
{
    public class RouteBuilder:IRouteBuilder
    {

        public IApplicationBuilder ApplicationBuilder { get; }

        public IRouter DefaultHandler { get; set; }

        public IServiceProvider ServiceProvider { get; }

        public IList<IRouter> Routes { get; }
        public RouteBuilder(IApplicationBuilder applicationBuilder)
    : this(applicationBuilder, defaultHandler: null)
        {
        }

        public RouteBuilder(IApplicationBuilder applicationBuilder, IRouter defaultHandler)
        {
            if (applicationBuilder == null)
            {
                throw new ArgumentNullException(nameof(applicationBuilder));
            }

            //if (applicationBuilder.ApplicationServices.GetService(typeof(RoutingMarkerService)) == null)
            //{
            //    throw new InvalidOperationException(Resources.FormatUnableToFindServices(
            //        nameof(IServiceCollection),
            //        nameof(RoutingServiceCollectionExtensions.AddRouting),
            //        "ConfigureServices(...)"));
            //}

            ApplicationBuilder = applicationBuilder;
            DefaultHandler = defaultHandler;
            ServiceProvider = applicationBuilder.ApplicationServices;

            Routes = new List<IRouter>();
        }
        public IRouter Build()
        {
            var routeCollection = new RouteCollection();
            foreach(var route in Routes)
            {
                routeCollection.Add(route);
            }
            return routeCollection;
        }

    }
}
