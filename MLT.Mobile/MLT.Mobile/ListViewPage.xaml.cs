using MLT.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.MultiSelectListView;
using Xamarin.Forms.Xaml;

namespace MLT.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage : ContentPage
    {
        public MultiSelectObservableCollection<StringForListView> ShowItems { get; set; }
        public IEnumerable<string> resultList;
        public EventHandler ResultSucceeded;

        public ListViewPage(IEnumerable<string> items, IEnumerable<string> selectedItems)
        {
            InitializeComponent();

            ShowItems = new MultiSelectObservableCollection<StringForListView>();
            foreach (var current in items)
            {
                var temp = new SelectableItem<StringForListView>(new StringForListView { Item = current });
                if ((selectedItems != null) && selectedItems.Contains(current))
                {
                    temp.IsSelected = true; 
                }
                ShowItems.Add(temp);                                 
            }
            this.BindingContext = this;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            resultList = new List<string>();
            foreach (var current in ShowItems)
            {
                if (current.IsSelected)
                {              
                    ((List<string>)resultList).Add(current.Data.Item);
                }
            }
            ResultSucceeded(resultList, EventArgs.Empty);
            var entrancePlace = await Navigation.PopModalAsync();
        }
    }
}