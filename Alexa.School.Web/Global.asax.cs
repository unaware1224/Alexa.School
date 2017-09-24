using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Alexa.School.Web
{
    public class WebApiApplication : HttpApplication
    {
        #region Methods

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(configurationCallback: WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(filters: GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(routes: RouteTable.Routes);
        }

        #endregion
    }
}