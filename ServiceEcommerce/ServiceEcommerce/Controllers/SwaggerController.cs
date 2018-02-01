using System.Web.Configuration;
using System.Web.Http;

namespace ServiceEcommerce.Controllers
{
    public class SwaggerController : ApiController
    {
        public string GetKey()
        {
            return WebConfigurationManager.AppSettings["apikey"];
        }
    }
}
