using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ColidColorlib.Startup))]
namespace ColidColorlib
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
