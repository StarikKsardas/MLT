using MLT.Desktop.AppUsers.Contracts.Validators;
using System;
using System.ComponentModel;
using System.Linq;

namespace MLT.Desktop.AppUsers.Contracts.ViewModels
{
    public class LoginView: INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly LoginValidator loginValidator;
        public LoginView()
        {
            this.loginValidator = new LoginValidator();
        }

        private string login;
        public string Login { get => login ; set { login = value; OnPropertyChanged(nameof(Login)); } }
        private string password;
        public string Password { get => password; set { password = value; OnPropertyChanged(nameof(Password)); } }

        public string this[string columnName]
        {
            get
            {
                var firstOrDefault = loginValidator.Validate(this).Errors.FirstOrDefault(lol => lol.PropertyName == columnName);
                if (firstOrDefault != null)
                    return loginValidator != null ? firstOrDefault.ErrorMessage : "";
                return "";
            }
        }

        public string Error
        {
            get
            {
                if (loginValidator != null)
                {
                    var results = loginValidator.Validate(this);
                    if (results != null && results.Errors.Any())
                    {
                        var errors = string.Join(Environment.NewLine, results.Errors.Select(x => x.ErrorMessage).ToArray());
                        return errors;
                    }
                }
                return string.Empty;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
