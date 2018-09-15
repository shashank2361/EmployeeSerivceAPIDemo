using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiContrib.Formatting.Jsonp;

namespace EmployeeSerivce
{
    public static class WebApiConfig
    {

        public class CustomJsonFormatter : JsonMediaTypeFormatter
        {
            public CustomJsonFormatter()
            {
                this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            }

            public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
            {
                base.SetDefaultContentHeaders(type, headers, mediaType);
                headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
        }



        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            config.Formatters.Add(new CustomJsonFormatter());
            //config.Formatters.Remove(config.Formatters.XmlFormatter);
            // config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
            //config.Formatters.JsonFormatter.SerializerSettings.Formatting =
            //                 Newtonsoft.Json.Formatting.Indented;
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
            //    new CamelCasePropertyNamesContractResolver();

            // This is when to use JsonP for cross orign request
            //var JsonPformattor = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter  );
            //config.Formatters.Insert(0, JsonPformattor);

            // This is to use cors for cross origin request
          // EnableCorsAttribute cors = new EnableCorsAttribute("http://localhost:51735", "*", "*");

            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors();
            config.Filters.Add(new RequireHttpsAttribute());
            config.Filters.Add(new BasicAuthenticationAttribute());
 
        }
    }
}
