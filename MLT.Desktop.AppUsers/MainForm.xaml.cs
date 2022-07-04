using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MLT.Desktop.AppUsers.Contracts.ViewModels;
using MLT.Domain.Contracts.InfoModels;
using MLT.Domain.Contracts.Services;


namespace MLT.Desktop.AppUsers
{
    public partial class MainForm : Window
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly ILogger logger;
        public int userId;
        public string login;
        
        private IEnumerable<UserView> userViews = new List<UserView>();

        public MainForm(IUserService userService, IMapper mapper, ILogger<MainForm> logger)
        {
            InitializeComponent();
            this.userService = userService;
            this.mapper = mapper;
            this.logger = logger;
            UpdateUserViews();            
        }

        public void Initialize()
        {
            UserName.Content = $"Пользователь: {login}";
            logger.LogInformation($"{UserName.Content} login to DB");
        }
        

        private void UpdateUserViews()
        {
            var usersInfos = userService.GetAllUsers();
            userViews = mapper.Map<IEnumerable<UserInfo>, IEnumerable<UserView>>(usersInfos);
            this.UsersTable.ItemsSource = userViews;
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentUserView = (UserView)UsersTable.SelectedItem;
                if (currentUserView != null)
                {
                    var userForm = ((App)Application.Current).ServiceProvider.GetService<UserForm>();
                    var userView = ((List<UserView>)userViews).FirstOrDefault(p => p.Id == currentUserView.Id);
                    userForm.Initiialize(userView);
                    this.IsEnabled = false;
                    if (userForm.ShowDialog().Value)
                    {
                        UpdateUserViews();
                    }
                    this.IsEnabled = true;
                }
                else
                {
                    throw new System.Exception();
                }

            }
            catch
            {
                MessageBox.Show("Выберите пользователя", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            var userForm = ((App)Application.Current).ServiceProvider.GetService<UserForm>();
            this.IsEnabled = false;
            if (userForm.ShowDialog().Value)
            {
                UpdateUserViews();
            }
            this.IsEnabled = true;
        }

        private void Button_Click_Remove(object sender, RoutedEventArgs e)
        {

            try
            {
                var currentUserView = (UserView)UsersTable.SelectedItem;
                if (currentUserView != null)
                {
                    if (MessageBox.Show("Вы уверены что хотите удалить пользователя?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Hand) == MessageBoxResult.Yes)
                    {
                        userService.Remove(currentUserView.Id);
                        logger.LogInformation($"{login} удалил пользователя {currentUserView.Login}");
                        UpdateUserViews();
                    }
                }
                else
                {
                    throw new System.Exception();
                }

            }
            catch
            {
                MessageBox.Show("Выберите пользователя", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void UsersTable_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                Button_Click_Edit(null, null);
            }
        }
    }
}
