using MLT.Desktop.AppUsers.Helpers;
using MLT.Domain.Contracts.InfoModels;
using MLT.Domain.Contracts.Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace MLT.Desktop.AppUsers
{
    public partial class AtdForm : Window
    {
        private readonly IInformationService informationService;
        private readonly List<AtdInfo> fullList;
        private readonly AtdWorker atdWorker;
        private bool IsFormChange = true;
        private AtdInfo startValue = null;

        public AtdInfo ResultValue { get; private set; }
        public AtdInfo StartValue { 
            get 
            {
                return startValue;
            } 
            set 
            {
                if (!string.IsNullOrEmpty(value?.Code))
                {
                    MakeChange(value);
                }
            } 
        }


        public AtdForm(IInformationService informationService)
        {
            InitializeComponent();
            this.informationService = informationService;
            fullList = (List<AtdInfo>)this.informationService.GetAllAtds();
            atdWorker = new AtdWorker(fullList);
            Countries.ItemsSource = atdWorker.Countries;
            Regions.ItemsSource = atdWorker.Regions;
            Districts.ItemsSource = atdWorker.Districts;
            Cities.ItemsSource = atdWorker.Cities;
        }



        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            var text = (string)e.DataObject.GetData(typeof(string));
            if (TextboxInput.IsDigits(text) && TextboxInput.IsNormalLenght(text) && TextboxInput.IsNormalLenght(((TextBox)sender).Text+text))
            {
                return;
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextboxInput.IsDigits(e.Text);
            if (e.Handled)
            {
                return;
            }
            e.Handled = !TextboxInput.IsNormalLenght(((TextBox)sender).Text);
        }

        private void All_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsFormChange)
            {
                var item = (AtdInfo)((ComboBox)sender).SelectedItem;
                MakeChange(item);
                ResultValue = new AtdInfo { Code = item.Code, Lex = FullAtdLabel.Text.ToString() };
            }            
        }

        private void MakeChange(AtdInfo atdInfo)
        {
            ResultValue = atdInfo;
            IsFormChange = false; ;
            atdWorker.ReCalc(atdInfo);
            Countries.ItemsSource = atdWorker.Countries;
            Regions.ItemsSource = atdWorker.Regions;
            Districts.ItemsSource = atdWorker.Districts;
            Cities.ItemsSource = atdWorker.Cities;
            Countries.SelectedItem = atdWorker.CurrentCountry;
            Regions.SelectedItem = atdWorker.CurrentRegion;
            Districts.SelectedItem = atdWorker.CurrentDistrict;
            Cities.SelectedItem = atdWorker.Cities;
            TextboxCode.Text = atdInfo.Code;
            FullAtdLabel.Text = "";
            if (atdWorker.CurrentCountry != null)
            {
                FullAtdLabel.Text += $"{atdWorker.CurrentCountry.Lex} ";
            }
            if (atdWorker.CurrentRegion != null)
            {
                FullAtdLabel.Text += $"{atdWorker.CurrentRegion.Lex} ";
            }
            if (atdWorker.CurrentDistrict != null)
            {
                FullAtdLabel.Text += $"{atdWorker.CurrentDistrict.Lex} ";
            }
            if (atdWorker.CurrentCity != null)
            {
                FullAtdLabel.Text += $"{atdWorker.CurrentCity.Lex}";
            }
            IsFormChange = true;            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TextboxCode.Text.Length == 10)
            {                                
                MakeChange(new AtdInfo { Code = TextboxCode.Text });
                ResultValue = new AtdInfo { Code = TextboxCode.Text, Lex = FullAtdLabel.Text.ToString() };
            }
            else
            {
                MessageBox.Show("Code isn't valid");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
