using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Knowledgeable.Startup))]
namespace Knowledgeable
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
