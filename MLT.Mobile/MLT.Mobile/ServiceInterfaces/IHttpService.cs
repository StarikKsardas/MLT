using MLT.Mobile.Helpers;
using System.Threading.Tasks;

namespace MLT.Mobile.ServiceInterfaces
{
    public interface IHttpService
    {
        Task<string> CallHttpMethodAsync(HttpInfo httpInfo);
    }
}
