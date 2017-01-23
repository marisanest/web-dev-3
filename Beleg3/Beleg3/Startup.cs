using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Beleg3.Startup))]
namespace Beleg3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
