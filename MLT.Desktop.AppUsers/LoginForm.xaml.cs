using MLT.Desktop.AppUsers.Contracts.ViewModels;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using MLT.Domain.Contracts.Services;
using AutoMapper;
using MLT.Domain.Contracts.InfoModels;
using Microsoft.Extensions.Logging;

namespace MLT.Desktop.AppUsers
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        private readonly LoginView loginView;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly ILogger<LoginForm> logger;

        public LoginForm(IUserService userService, IMapper mapper, ILogger<LoginForm> logger)
        {
            InitializeComponent();
            this.loginView = RestoreContextFromSettings();
            this.DataContext = loginView;
            this.userService = userService;
            this.mapper = mapper;
            this.logger = logger;          
        }
       

        private void Button_Click(object sender, RoutedEventArgs e)
        {            

            var current = (LoginView)DataContext;
            if (string.IsNullOrEmpty(current.Error))
            {
                var userInfo = mapper.Map<LoginView, UserInfo>(loginView);
                var id = userService.ConnectToDb(userInfo);
                if (id != 0)
                {
                    SaveContextToSettings(current);
                    var mainForm = ((App)Application.Current).ServiceProvider.GetService<MainForm>();
                    mainForm.userId = id;
                    mainForm.login = current.Login;
                    mainForm.Initialize();
                    mainForm.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Пользователь не найден", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(current.Error,"Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private LoginView RestoreContextFromSettings()
        {
            var result = new LoginView
            {                
                Login = Startup.Default.Login,                
            };
            return result;
        }

        private void SaveContextToSettings(LoginView loginView)
        {           
            Startup.Default.Login = loginView.Login;            
            Startup.Default.Save();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { 
                loginView.Password = ((PasswordBox)sender).Password; 
            }
        }

        private void PasswordBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Button_Click(null, null);
            }
        }
    }
}
