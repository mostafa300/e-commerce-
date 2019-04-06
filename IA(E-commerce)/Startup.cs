using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IA_E_commerce_.Startup))]
namespace IA_E_commerce_
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
