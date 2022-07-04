using MLT.Desktop.AppUsers.Contracts.Validators;
using System;
using System.ComponentModel;
using System.Linq;


namespace MLT.Desktop.AppUsers.Contracts.ViewModels
{
    public class UserView: INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly UserValidator userValidator;

        public UserView()
        {
            this.userValidator = new UserValidator();
        }
        
        public int Id { get; set; } 
        private string lastName;
        public string LastName {get => lastName; set { lastName = value; OnPropertyChanged(nameof(LastName)); } }
        private string firstName;
        public string FirstName { get => firstName; set { firstName = value; OnPropertyChanged(nameof(FirstName)); } }
        private string midName { get; set; }
        public string MidName { get => midName; set { midName = value; OnPropertyChanged(nameof(MidName)); } }
        private string login;
        public string Login { get => login; set { login = value; OnPropertyChanged(nameof(Login)); } }
        private string phone { get; set; }
        public string Phone { get => phone; set { phone = value; OnPropertyChanged(nameof(Phone)); } }
        private string place { get; set; }
        public string Place { get => place; set { place = value; OnPropertyChanged(nameof(Place)); } }
        public string PlaceCode { get; set; }
        public string PlaceCodeLex { get; set; }
        private string password; 
        public string Password { get => password; set { password = value; OnPropertyChanged(nameof(Password)); } }
        private string confirmPassword;
        public string ConfirmPassword { get => confirmPassword; set { confirmPassword = value; OnPropertyChanged(nameof(ConfirmPassword)); } }
        private string phoneId;
        public string PhoneId { get => phoneId; set { phoneId = value; OnPropertyChanged(nameof(PhoneId)); } }

        public string this[string columnName]
        {
            get
            {
                var firstOrDefault = userValidator.Validate(this).Errors.FirstOrDefault(lol => lol.PropertyName == columnName);
                if (firstOrDefault != null)
                    return userValidator != null ? firstOrDefault.ErrorMessage : "";
                return "";
            }
        }

        public string Error
        {
            get
            {
                if (userValidator != null)
                {
                    var results = userValidator.Validate(this);
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
