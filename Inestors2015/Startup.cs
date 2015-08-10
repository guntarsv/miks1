using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Investors2015.Startup))]
namespace Investors2015
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
