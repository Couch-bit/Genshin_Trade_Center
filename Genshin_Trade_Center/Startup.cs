using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Genshin_Trade_Center.Startup))]
namespace Genshin_Trade_Center
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
