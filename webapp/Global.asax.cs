namespace SmartAdminMvc
{
    using System;
    using System.Web;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Web.Optimization;
    using System.Web.Mvc;
    using System.Web.Routing;
    using SmartAdminMvc.App_Start;
    using System.Web.Http;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AuthConfig.RegisterAuth();

            System.Data.Entity.Database.SetInitializer<Models.DBEntity>(null);

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var cookie = Request.Cookies["LanguagePreference"];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                try
                {
                    var culture = CultureInfo.GetCultureInfo(cookie.Value);
                    Thread.CurrentThread.CurrentUICulture = culture;
                    CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                    newCulture.DateTimeFormat.ShortDatePattern = "dd-MMM-yyyy";
                    newCulture.DateTimeFormat.DateSeparator = "-";
                    Thread.CurrentThread.CurrentCulture = newCulture;
                }
                catch (CultureNotFoundException)
                {
                    // ignore
                }
            }
        }
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }
    }
}