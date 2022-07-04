using Android.App;
using Android.Provider;
using MLT.Mobile.Droid.Services;
using MLT.Mobile.ServiceInterfaces;


[assembly: Xamarin.Forms.Dependency(typeof(DeviceService))]
namespace MLT.Mobile.Droid.Services
{
    class DeviceService : IDeviceService
    {
        public string GetDeviceId()
        {
            var context = Application.Context;
            string id = Settings.Secure.GetString(context.ContentResolver, Settings.Secure.AndroidId);
            return id;
        }
    }
}