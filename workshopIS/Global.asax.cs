using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Spring.Web.Mvc;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace workshopIS
{
    public class MvcApplication : SpringMvcApplication
    {
        protected void Application_Start()
        {
            Data.Initialize(); // initialize static data class 
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected override IDependencyResolver BuildWebApiDependencyResolver()
        {
            var resolver = base.BuildWebApiDependencyResolver();

            var springResolver = resolver as SpringWebApiDependencyResolver;

            return resolver;
        }
    }
}
