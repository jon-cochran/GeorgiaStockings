using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GeorgiaStockings.Startup))]
namespace GeorgiaStockings
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
