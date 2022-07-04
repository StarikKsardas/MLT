using MLT.Mobile.Helpers;
using MLT.Mobile.ServiceInterfaces;
using MLT.Web.Contracts.WebModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MLT.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnswersPage : ContentPage
    {
        private readonly HttpInfo httpInfo;
        private readonly IHttpService httpService;
        private IEnumerable<AnswerWeb> answerWebs;

        public AnswersPage(HttpInfo httpInfo)
        {
            InitializeComponent();
            this.httpInfo = httpInfo;
            this.httpService = DependencyService.Get<IHttpService>();            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            httpInfo.ControllerName = "answer";
            httpInfo.JsonData = null;
            httpInfo.MethodName = "getuserlatents";
            httpInfo.MethodType = RestSharp.Method.Get;

            var stringResult = await httpService.CallHttpMethodAsync(httpInfo);
            answerWebs = JsonConvert.DeserializeObject<IEnumerable<AnswerWeb>>(stringResult);

            var parent = new StackLayout();
            foreach (var current in answerWebs)
            {
                
                string typeLatent = !current.IsPalm ? "След пальца" : "След лад.";
                Expander expander = new Expander
                {
                    Header = new Label
                    {
                        Text = $"{current.RegistrationNumber}, {current.LatentNumber}, {typeLatent}",
                        FontAttributes = FontAttributes.Bold,
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
                    }
                };

                var queryStack = new StackLayout();

                foreach (var query in current.Queries)
                {
                    queryStack.Children.Add(new Label
                    {
                        Text = $"{query.QueryId} {query.Status}",
                        FontAttributes = FontAttributes.Italic
                    });
                }

                expander.Content = queryStack;
                parent.Children.Add(expander);
            }
            mainScroll.Content = parent;
        }




    }
}