using System;
using Xamarin.Forms;
using MLT.Mobile.Helpers;
using MLT.Mobile.ServiceInterfaces;
using MLT.Web.Contracts.WebModels;
using Newtonsoft.Json;
using AutoMapper;
using MLT.Mobile.Models;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MLT.Mobile
{
    public partial class MainPage : ContentPage
    {
        private readonly ConnectivityCheck connectivityCheck;
        private readonly IHttpService httpService;
        private readonly string deviceId;
        private readonly IProtectionService protectionService;
        private ConnectMoblie connectMobile;
        private HttpInfo httpInfo;

        public MainPage()
        {
            InitializeComponent();
            this.httpService = DependencyService.Get<IHttpService>();
            this.deviceId = DependencyService.Get<IDeviceService>().GetDeviceId();
            this.protectionService = DependencyService.Get<IProtectionService>();

            connectivityCheck = new ConnectivityCheck();
            hasInternet.Text = connectivityCheck.AccessType;
            connectionType.Text = connectivityCheck.ProfileType;
            connectivityCheck.Notify += ChangeConnectivity;

            this.connectMobile = RestoreContextFromSettings();
            connectMobile.PhoneId = deviceId;
            BindingContext = connectMobile;


        }

        private ConnectMoblie RestoreContextFromSettings()
        {
            connectMobile = new ConnectMoblie();
            if (Application.Current.Properties.ContainsKey("Login"))
            {
                connectMobile.Login = (string)Application.Current.Properties["Login"];
                connectMobile.Host = (string)Application.Current.Properties["Host"];
                connectMobile.Port = (string)Application.Current.Properties["Port"];
            }
            return connectMobile;
        }

        private async void SaveContextToSettings(ConnectMoblie connectMoblie)
        {
            if (Application.Current.Properties.ContainsKey("Login"))
            {
                Application.Current.Properties["Login"] = connectMoblie.Login;
                if (connectMoblie.Host != "8080")
                    Application.Current.Properties["Host"] = connectMoblie.Host;
                else
                    Application.Current.Properties["Host"] = "8080";
                Application.Current.Properties["Port"] = connectMoblie.Port;
            }
            else
            {
                Application.Current.Properties.Add("Login", connectMoblie.Login);
                if (connectMoblie.Host != "8080")
                    Application.Current.Properties.Add("Host", connectMoblie.Host);
                else
                    Application.Current.Properties.Add("Host", "8080");
                Application.Current.Properties.Add("Port", connectMoblie.Port);
            }
            await Application.Current.SavePropertiesAsync();
        }

        public void ChangeConnectivity()
        {
            hasInternet.Text = connectivityCheck.AccessType;
            connectionType.Text = connectivityCheck.ProfileType;
        }

        private void ViewConnectionProperties(object sender, EventArgs e)
        {
            uriEntry.IsVisible = !uriEntry.IsVisible;
            portEntry.IsVisible = !portEntry.IsVisible;
            uIdPhoneEntry.IsVisible = !uIdPhoneEntry.IsVisible;
            uIdPhone.IsVisible = !uIdPhone.IsVisible;
        }

        private void LockComponents()
        {
            mainStack.IsVisible = false;
            activityStack.IsVisible = true;
        }

        private void UnlockComponents()
        {
            mainStack.IsVisible = true;
            activityStack.IsVisible = false;
        }
        private async void TryConnect(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(connectMobile.Errors))
            {
                LockComponents();
                var userWeb = new UserWeb
                {
                    Login = connectMobile.Login.ToUpper().Trim(),
                    PhoneId = connectMobile.PhoneId,
                    PasswordEncrypt = protectionService.EncryptPassword(connectMobile.Password)
                };

                httpInfo = new HttpInfo
                {
                    Host = connectMobile.Host,
                    Port = connectMobile.Port,
                    Login = connectMobile.Login.Trim().ToUpper(),
                    PhoneId = connectMobile.PhoneId,
                    EncryptPassword = protectionService.EncryptPassword(connectMobile.Password),
                    ControllerName = "user",
                    MethodName = "signin",
                    MethodType = RestSharp.Method.Post,
                    JsonData = JsonConvert.SerializeObject(userWeb)
                };
                try
                {
                    var result = await httpService.CallHttpMethodAsync(httpInfo); 

                    if (result == "OK")
                    {
                        SaveContextToSettings(connectMobile);
                        connectMobile.Password = "";
                        await Navigation.PushModalAsync(new SelectPage(httpInfo));

                    }
                    else
                    {
                        await DisplayAlert("Error", result, "cancel");
                    }
                }
                catch (Exception exception)
                {
                    await DisplayAlert("Error", exception.Message, "cancel");
                }
                
            }
            else
            {
                await DisplayAlert("Validatation", connectMobile.Errors, "Ok");
            }
            UnlockComponents();
        }

        private async void uIdPhoneEntry_Focused(object sender, FocusEventArgs e)
        {
            ((Entry)sender).IsReadOnly = true;
            await Clipboard.SetTextAsync(connectMobile.PhoneId);
            await DisplayAlert("Copied", "Text copied", "Ok");
            ((Entry)sender).IsReadOnly = false;
        }
    }
}
