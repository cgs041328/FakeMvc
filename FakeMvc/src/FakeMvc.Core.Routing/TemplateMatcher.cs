using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeMvc.Core.Routing.Template;
using Microsoft.AspNet.Http;

namespace FakeMvc.Core.Routing
{
    public class TemplateMatcher
    {
        private RouteValueDictionary defaults;
        private RouteTemplate parsedTemplate;

        public TemplateMatcher(RouteTemplate parsedTemplate, RouteValueDictionary defaults)
        {
            this.parsedTemplate = parsedTemplate;
            this.defaults = defaults;
        }

        internal bool TryMatch(PathString requestPath, object values)
        {
            throw new NotImplementedException();
        }
    }
}
