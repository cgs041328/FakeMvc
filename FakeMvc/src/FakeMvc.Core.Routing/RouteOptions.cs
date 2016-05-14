using System;
using System.Collections.Generic;

namespace FakeMvc.Core.Routing
{
    public class RouteOptions
    {
        public bool AppendTrailingSlash { get; internal set; }
        public bool LowercaseUrls { get; internal set; }

        public class RouteOptions
        {
            /// <summary>
            /// Gets or sets a value indicating whether all generated URLs are lower-case.
            /// </summary>
            public bool LowercaseUrls { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether a trailing slash should be appended to the generated URLs.
            /// </summary>
            public bool AppendTrailingSlash { get; set; }

            private IDictionary<string, Type> _constraintTypeMap = GetDefaultConstraintMap();

            public IDictionary<string, Type> ConstraintMap
            {
                get
                {
                    return _constraintTypeMap;
                }
                set
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException(nameof(ConstraintMap));
                    }

                    _constraintTypeMap = value;
                }
            }

            private static IDictionary<string, Type> GetDefaultConstraintMap()
            {
                return new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
            {
                // Type-specific constraints
                //{ "int", typeof(IntRouteConstraint) },
                //{ "bool", typeof(BoolRouteConstraint) },
                //{ "datetime", typeof(DateTimeRouteConstraint) },
                //{ "decimal", typeof(DecimalRouteConstraint) },
                //{ "double", typeof(DoubleRouteConstraint) },
                //{ "float", typeof(FloatRouteConstraint) },
                //{ "guid", typeof(GuidRouteConstraint) },
                //{ "long", typeof(LongRouteConstraint) },

                //// Length constraints
                //{ "minlength", typeof(MinLengthRouteConstraint) },
                //{ "maxlength", typeof(MaxLengthRouteConstraint) },
                //{ "length", typeof(LengthRouteConstraint) },

                //// Min/Max value constraints
                //{ "min", typeof(MinRouteConstraint) },
                //{ "max", typeof(MaxRouteConstraint) },
                //{ "range", typeof(RangeRouteConstraint) },

                //// Regex-based constraints
                //{ "alpha", typeof(AlphaRouteConstraint) },
                //{ "regex", typeof(RegexInlineRouteConstraint) },

                //{"required", typeof(RequiredRouteConstraint) },
            };
            }
        }
    }
}