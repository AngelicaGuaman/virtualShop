using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PracticaNETRoP.Startup))]
namespace PracticaNETRoP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
