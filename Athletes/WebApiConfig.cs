using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Athletes
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new Host)
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    } 
}