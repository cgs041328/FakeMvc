using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeMvc.Core.Routing.Template
{
    public class RouteTemplate
    {
        private string routeTemplate;
        private List<TemplateSegment> segments;

        public RouteTemplate(string routeTemplate, List<TemplateSegment> segments)
        {
            this.routeTemplate = routeTemplate;
            this.segments = segments;
        }

        public string TemplateText { get; }

        public IList<TemplatePart> Parameters { get; }

        public IList<TemplateSegment> Segments { get; }
    }
}
