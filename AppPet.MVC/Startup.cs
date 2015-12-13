using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppPet.MVC.Startup))]
namespace AppPet.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
