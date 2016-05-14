using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeMvc.Core.Routing.Template
{
    public class InlineConstraint
    {
        public string Constraint { get; }
        public InlineConstraint(string constraint)
        {
            if (constraint == null)
            {
                throw new ArgumentNullException(nameof(constraint));
            }

            Constraint = constraint;
        }
    }
}
