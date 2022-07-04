using MLT.Mobile.Helpers;
using MLT.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MLT.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectPage : ContentPage
    {
        private readonly HttpInfo httpInfo;
        
        public SelectPage(HttpInfo httpInfo)
        {
            InitializeComponent();
            this.httpInfo = httpInfo;            
        }

        private async void ChangePassword(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ChangePasswordPage(httpInfo));
        }

        private async void SendLatent(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LatentPage(httpInfo));
        }

        private async void CheckResults(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AnswersPage(httpInfo));
        }
    }
}