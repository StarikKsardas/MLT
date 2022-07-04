using System.Linq;
using Xamarin.Essentials;

namespace MLT.Mobile.Helpers
{
    public class ConnectivityCheck
    {
        
        public string AccessType { get; set; }
        public string ProfileType { get; set; }
        public delegate void ConnectiviryHandler();
        #nullable enable
        public event ConnectiviryHandler? Notify;
        #nullable disable
        public ConnectivityCheck()
        {
            Connectivity_ConnectivityChanged(null, null);
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }
        
        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            AccessType = Connectivity.NetworkAccess.ToString();
            ProfileType = Connectivity.ConnectionProfiles.FirstOrDefault().ToString();
            Notify?.Invoke();
        }
    }
}
