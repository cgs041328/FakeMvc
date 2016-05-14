namespace FakeMvc.Core.Routing
{
    public class VirtualPathData
    {
        private string url;

        public VirtualPathData(IRouter router, string url, RouteValueDictionary dataTokens)
        {
            Router = router;
            this.url = url;
            DataTokens = dataTokens;
        }

        public RouteValueDictionary DataTokens { get; internal set; }
        public IRouter Router { get; internal set; }
        public string VirtualPath { get; internal set; }
    }
}