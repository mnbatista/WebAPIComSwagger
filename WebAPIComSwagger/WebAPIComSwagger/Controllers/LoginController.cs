using Newtonsoft.Json.Linq;
using System.Web.Http;
namespace WebAPIComSwagger.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("Login")]
        public JObject loginService([FromBody] JObject loginJson)
        {
            JObject retJson = new JObject();
            string username = loginJson["username"].ToString();
            string password = loginJson["password"].ToString();
            if (username == "admin" && password == "admin")
            {
                retJson.Add(new JProperty("authentication ", "successful"));
            }
            else
            {
                retJson.Add(new JProperty("authentication ", "unsuccessful"));
            }
            return retJson;
        }
    }
}
