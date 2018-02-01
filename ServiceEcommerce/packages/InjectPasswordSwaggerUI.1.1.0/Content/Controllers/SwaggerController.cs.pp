using System.Web.Configuration;
using System.Web.Http;

namespace $rootnamespace$.Controllers
{
    public class SwaggerController : ApiController
    {
        public string GetKey()
        {
            return WebConfigurationManager.AppSettings["apikey"];
        }
    }
}
