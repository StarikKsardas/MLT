using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MLT.Desktop.AppUsers.Contracts.ViewModels;
using MLT.Desktop.AppUsers.Helpers;
using MLT.Domain.Contracts.InfoModels;
using MLT.Domain.Contracts.Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MLT.Desktop.AppUsers
{
    /// <summary>
    /// Interaction logic for UserForm.xaml
    /// </summary>
    public partial class UserForm : Window
    {
        public UserView userView;
        private IEnumerable<string> places;
        private readonly IInformationService informationService;
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly ILogger logger;
        private bool isUpdate = false;
        private const string PreviousPasswordLabel = "{Если не изменять, то останется текущий}";
        private const string DefaultPassword = "********";        

        public UserForm(IInformationService informationService, IMapper mapper, IUserService userService, ILogger<UserForm> logger)
        {
            InitializeComponent();
            if (userView == null)
            {
                userView = new UserView();
            }
            this.informationService = informationService;
            this.userService = userService;
            this.mapper = mapper;
            this.DataContext = userView;
            this.logger = logger;
            places = informationService.GetAllAbrPlaces();
            Places.ItemsSource = places;
        }

        public void Initiialize(UserView userView)
        {
            this.userView = userView;
            this.DataContext = this.userView;
            isUpdate = true;
            LabelPassword.Content += $" {PreviousPasswordLabel}";
            LabelRepeatPassword.Content += $" {PreviousPasswordLabel}";
            Password.Password = DefaultPassword;
            RepeatPassword.Password = DefaultPassword;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var atdForm = ((App)Application.Current).ServiceProvider.GetService<AtdForm>();
            atdForm.StartValue = new AtdInfo { Code = userView.PlaceCode };
            if (atdForm.ShowDialog().Value)
            {
                userView.PlaceCode = atdForm.ResultValue.Code;
                userView.PlaceCodeLex = atdForm.ResultValue.Lex;
                this.AtdLabel.Text = userView.PlaceCodeLex;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                userView.Password = ((PasswordBox)sender).Password;
            }
        }

        private void PasswordBox_PasswordChanged_1(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                userView.ConfirmPassword = ((PasswordBox)sender).Password;
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
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {            
            var current = (UserView)this.DataContext;
            if (string.IsNullOrEmpty(current?.Error))
            {                             
                userView = current;
                var userInfo = mapper.Map<UserView, UserInfo>(userView);
                if (!isUpdate)
                {
                    if (userService.ValidateExist(userInfo))
                    {
                        MessageBox.Show("User Exist!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    userService.Add(userInfo);
                    logger.LogInformation($"{userInfo.Login} был добавлен");
                    this.DialogResult = true;
                }
                else
                {
                    if (userInfo.Password == DefaultPassword)
                    {
                        userService.Update(userInfo, false);
                    }
                    else
                    {
                        userService.Update(userInfo, true);
                    }
                    logger.LogInformation($"{userInfo.Login} был обновлен");
                    this.DialogResult = true;
                }
            }
            else
            {
                MessageBox.Show(current.Error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
