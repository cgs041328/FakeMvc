using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeMvc.Core.Routing.Template
{
    public static class TemplateParser
    {
        private const char Separator = '/';
        private const char OpenBrace = '{';
        private const char CloseBrace = '}';
        private const char EqualsSign = '=';
        private const char QuestionMark = '?';
        private const char Asterisk = '*';
        private const string PeriodString = ".";
        public static RouteTemplate Parse(string routeTemplate)
        {
            routeTemplate = routeTemplate ?? string.Empty;
            if (IsInvalidRouteTemplate(routeTemplate))
            {
                throw new ArgumentException(nameof(routeTemplate));
            }
            var context = new TemplateParserContext(routeTemplate);
            var segments = new List<TemplateSegment>();
            while(context.Next())
            {
                if(context.Current == Separator)
                {
                    throw new ArgumentException(nameof(routeTemplate));
                }
                else
                {
                       //if (!context.ParameterNames segments))
                       // {
                       //     throw new ArgumentException(context.Error, nameof(routeTemplate));
                       // }
                }
            }
            return new RouteTemplate(routeTemplate, segments);
        }
        //private static bool ParseSegment(this TemplateParserContext context, List<TemplateSegment> segments)
        //{
        //    var segment = new TemplateSegment();

        //}
        private static bool ParseParameter(this TemplateParserContext context,TemplateSegment segment)
        {
            context.Mark();
            while (true)
            {
                if (context.Current == OpenBrace)
                {
                    // This is an open brace inside of a parameter, it has to be escaped
                    if (context.Next())
                    {
                        if (context.Current != OpenBrace)
                        {
                            // If we see something like "{p1:regex(^\d{3", we will come here.
                            context.Error = "UnescapedBrace";
                            return false;
                        }
                    }
                    else
                    {
                        // This is a dangling open-brace, which is not allowed
                        // Example: "{p1:regex(^\d{"
                        context.Error = "MismatchedParameter";
                        return false;
                    }
                }
                else if (context.Current == CloseBrace)
                {
                    if (!context.Next())
                    {
                        context.Back();
                        break;
                    }
                    if (context.Current == CloseBrace)
                    {

                    }
                    else
                    {
                        // This is the end of the parameter
                        context.Back();
                        break;
                    }
                    if (!context.Next())
                    {
                        // This is a dangling open-brace, which is not allowed
                        context.Error = "MismatchedParameter";
                        return false;
                    }
                }
            }
            var rawParameter = context.Capture();
            var decoded = rawParameter.Replace("}}", "}").Replace("{{", "{");
            return true;
        }
        private static bool IsInvalidRouteTemplate(string routeTemplate)
        {
            return routeTemplate.StartsWith("~", StringComparison.Ordinal) || routeTemplate.StartsWith("/", StringComparison.Ordinal);
        }
        private class TemplateParserContext
        {
            private readonly string _template;
            private int _index;
            private int? _mark;
            private HashSet<string> _parameterNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            public TemplateParserContext(string routeTemplate)
            {
                _template = routeTemplate;
                _index = -1;
            }
            public char Current
            {
                get { return _index < _template.Length && _index >= 0 ? _template[_index] : (char)0; }
            }
            public string Error { get; set; }
            public HashSet<string> ParameterNames
            {
                get { return _parameterNames; }
            }
            public bool Back()
            {
                return --_index >= 0;
            }
            public bool Next()
            {
                return ++_index < _template.Length;
            }
            public void Mark()
            {
                _mark = _index;
            }
            public string Capture()
            {
                if (_mark.HasValue)
                {
                    var value = _template.Substring(_mark.Value, _index - _mark.Value);
                    _mark = null;
                    return value;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
