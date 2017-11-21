using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppAdvogados.Startup))]
namespace AppAdvogados
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
