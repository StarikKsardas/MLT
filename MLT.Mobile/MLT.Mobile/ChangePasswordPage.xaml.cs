using MLT.Mobile.Helpers;
using MLT.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using MLT.Mobile.ServiceInterfaces;
using MLT.Web.Contracts.WebModels;

namespace MLT.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPage : ContentPage
    {
        private HttpInfo httpInfo;
        private ChangePasswordMobile changePasswordMobile;
        private readonly IHttpService httpService;
        private readonly IProtectionService protectionService;

        public ChangePasswordPage(HttpInfo httpInfo)
        {
            InitializeComponent();
            this.httpService = DependencyService.Get<IHttpService>();
            this.protectionService = DependencyService.Get<IProtectionService>();
            this.httpInfo = httpInfo;
            this.changePasswordMobile = new ChangePasswordMobile();
            BindingContext = changePasswordMobile;
        }

        public async void TryChangePassword(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(changePasswordMobile.Errors))
            {
                var userChangePassword = new UserChangePasswordWeb
                {
                    Login = httpInfo.Login,
                    NewPasswordEncrypt = protectionService.EncryptPassword(changePasswordMobile.NewPassword),
                    OldPasswordEncrypt = protectionService.EncryptPassword(changePasswordMobile.OldPassword),
                    PhoneId = httpInfo.PhoneId
                };
                httpInfo.ControllerName = "user";
                httpInfo.JsonData = JsonConvert.SerializeObject(userChangePassword);
                httpInfo.MethodName = "changepassword";
                httpInfo.MethodType = RestSharp.Method.Post;
                try
                {
                    var result = await httpService.CallHttpMethodAsync(httpInfo);                
                    if (result == "OK")
                    {
                        await DisplayAlert("Success", "Password was changed successfuly", "Ok");
                        await Navigation.PopModalAsync();
                    }
                    else
                    {
                        await DisplayAlert("Error", result, "Ok");
                    }
                }
                catch(Exception exception)                
                {
                    await DisplayAlert("Error", exception.Message, "Ok");
                }
            }
            else
            {
                await DisplayAlert("Validatation", changePasswordMobile.Errors, "Ok");
            }
        }
    }
}