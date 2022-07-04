using MLT.Mobile.Droid.Services;
using MLT.Mobile.Helpers;
using MLT.Mobile.ServiceInterfaces;
using RestSharp;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(HttpService))]
namespace MLT.Mobile.Droid.Services
{
    public class HttpService : IHttpService
    {
        private string CreateUri(HttpInfo httpInfo)
        {            
            var uri = $"{httpInfo.Host}:{httpInfo.Port}/{httpInfo.ControllerName}/{httpInfo.MethodName}/";
            return uri;
        }

        public async Task<string> CallHttpMethodAsync(HttpInfo httpInfo)
        {
            var uri = CreateUri(httpInfo);
            var client = new RestClient(uri);
            var request = new RestRequest("",httpInfo.MethodType);
            request.AddHeader("Login", httpInfo.Login);
            request.AddHeader("EncryptPassword", httpInfo.EncryptPassword);
            request.AddHeader("PhoneId", httpInfo.PhoneId);
            if (!string.IsNullOrEmpty(httpInfo.JsonData))
            {
                request.AddBody(httpInfo.JsonData, "application/json");
            }
            RestResponse restResponse = await client.ExecuteAsync(request);
            return restResponse.Content.Trim().Replace("\\", "").Trim('\"');
        }
    }
}
