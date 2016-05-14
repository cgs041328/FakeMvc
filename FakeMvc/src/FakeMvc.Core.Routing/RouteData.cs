using System;
using System.Collections.Generic;

namespace FakeMvc.Core.Routing
{
    public class RouteData
    {
        public RouteValueDictionary DataTokens => new RouteValueDictionary();
        public IList<IRouter> Routers => new List<IRouter>();
        public RouteValueDictionary Values => new RouteValueDictionary();
        public RouteData()
        {

        }
        public RouteDataSnapshot PushState(IRouter router, RouteValueDictionary values, RouteValueDictionary dataTokens)
        {
            var snapshot = new RouteDataSnapshot(
                this,
                DataTokens?.Count > 0 ? new RouteValueDictionary(DataTokens) : null,
                Routers?.Count > 0 ? new List<IRouter>(Routers) : null,
                Values?.Count > 0 ? new RouteValueDictionary(Values) : null);

            if (router != null)
            {
                Routers.Add(router);
            }

            if (values != null)
            {
                foreach (var kvp in values)
                {
                    if (kvp.Value != null)
                    {
                        Values[kvp.Key] = kvp.Value;
                    }
                }
            }

            if (dataTokens != null)
            {
                foreach (var kvp in dataTokens)
                {
                    DataTokens[kvp.Key] = kvp.Value;
                }
            }

            return snapshot;
        }
        public struct RouteDataSnapshot
        {
            private readonly RouteData _routeData;
            private readonly RouteValueDictionary _dataTokens;
            private readonly IList<IRouter> _routers;
            private readonly RouteValueDictionary _values;
            public RouteDataSnapshot(
    RouteData routeData,
    RouteValueDictionary dataTokens,
    IList<IRouter> routers,
    RouteValueDictionary values)
            {
                if (routeData == null)
                {
                    throw new ArgumentNullException(nameof(routeData));
                }

                _routeData = routeData;
                _dataTokens = dataTokens;
                _routers = routers;
                _values = values;
            }
            public void Restore()
            {
                if (_routeData.DataTokens == null && _dataTokens == null)
                {
                    // Do nothing
                }
                else if (_dataTokens == null)
                {
                    _routeData.DataTokens.Clear();
                }
                else
                {
                    _routeData.DataTokens.Clear();

                    foreach (var kvp in _dataTokens)
                    {
                        _routeData.DataTokens.Add(kvp.Key, kvp.Value);
                    }
                }

                if (_routeData.Routers == null && _routers == null)
                {
                    // Do nothing
                }
                else if (_routers == null)
                {
                    _routeData.Routers.Clear();
                }
                else
                {
                    _routeData.Routers.Clear();

                    for (var i = 0; i < _routers.Count; i++)
                    {
                        _routeData.Routers.Add(_routers[i]);
                    }
                }

                if (_routeData.Values == null && _values == null)
                {
                    // Do nothing
                }
                else if (_values == null)
                {
                    _routeData.Values.Clear();
                }
                else
                {
                    _routeData.Values.Clear();

                    foreach (var kvp in _values)
                    {
                        _routeData.Values.Add(kvp.Key, kvp.Value);
                    }
                }
            }
        }
    }
}