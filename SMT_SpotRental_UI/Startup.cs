using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SMT.SpotRental.UI.Startup))]
namespace SMT.SpotRental.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
