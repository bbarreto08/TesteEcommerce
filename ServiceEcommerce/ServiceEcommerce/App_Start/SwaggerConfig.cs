using System.Web.Http;
using WebActivatorEx;
using ServiceEcommerce;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace ServiceEcommerce
{    
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        
                        c.SingleApiVersion("v1", "ServiceEcommerce");
                        c.IncludeXmlComments(string.Format(@"{0}\bin\ServiceEcommerce.xml", 
                            System.AppDomain.CurrentDomain.BaseDirectory));

                    })
                .EnableSwaggerUi(c =>
                    {
                      
                    });
        }
    }
}
