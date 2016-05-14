using System.Collections.Generic;

namespace FakeMvc.Core.Routing.Template
{
    public class TemplatePart
    {
        public bool IsCatchAll { get; private set; }
        public bool IsLiteral { get; private set; }
        public bool IsParameter { get; private set; }
        public bool IsOptional { get; private set; }
        public bool IsOptionalSeperator { get; set; }
        public string Name { get; private set; }
        public string Text { get; private set; }
        public object DefaultValue { get; private set; }
        public IEnumerable<InlineConstraint> InlineConstraints { get; private set; }
    }
}