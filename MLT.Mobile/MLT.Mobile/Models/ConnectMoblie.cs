using MLT.Mobile.Validators;
using RestSharp;
using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace MLT.Mobile.Models
{
    
    public class ConnectMoblie: INotifyPropertyChanged
    {
        private readonly ConnectValidator connectValidator;
        

        public string Errors { get; set; }

        public ConnectMoblie()
        {
            this.connectValidator = new ConnectValidator();
        }

        private string host;
        public string Host { get { return host; } set { host = value; OnPropertyChanged(Host); } }

        private string port;
        public string Port { get { return port; } set { port = value; OnPropertyChanged(Port); } }

        private string login;
        public string Login { get { return login; } set { login = value; OnPropertyChanged(Login); } }

        private string password;
        public string Password { get { return password; } set { password = value; OnPropertyChanged(Password); } }

        private string phoneId;
        public string PhoneId { get { return phoneId; } set { phoneId = value; OnPropertyChanged(PhoneId); } }
                              
        private Command connectCommand;
        public Command ConnectCommand
        {
            get
            {
                return connectCommand ?? (connectCommand = new Command(ExecuteConnectCommand));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void ExecuteConnectCommand()
        {
            Errors = "";
            var result = connectValidator.Validate(this);
            if (result.IsValid)
            {
                Errors = string.Empty;
            }
            else
            {
                var value = "";
                foreach (var error in result.Errors)
                {
                    value += error.ErrorMessage + Environment.NewLine;
                }
                Errors = value;
                OnPropertyChanged(nameof(Errors));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
