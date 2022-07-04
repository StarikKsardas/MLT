using MLT.Mobile.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace MLT.Mobile.Models
{
    public class ChangePasswordMobile: INotifyPropertyChanged
    {
        private ChangePasswordValidator changePasswordValidator;
       
        public string Errors { get; set; }

        public ChangePasswordMobile()
        {
            this.changePasswordValidator = new ChangePasswordValidator();
        }

        private string oldPassword;
        public string OldPassword { get { return oldPassword; } set { oldPassword = value; OnPropertyChanged(nameof(OldPassword)); } }
        private string newPassword;
        public string NewPassword { get { return newPassword; } set { newPassword = value; OnPropertyChanged(nameof(NewPassword)); } }
        private string confirmedPassword;        
        public string ConfirmedPassword { get { return confirmedPassword; } set { confirmedPassword = value; OnPropertyChanged(nameof(ConfirmedPassword)); } }

        private Command changePasswordCommand;
        public Command ChangePasswordCommand
        {
            get
            {
                return changePasswordCommand ?? (changePasswordCommand = new Command(ExecuteConnectCommand));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        public void ExecuteConnectCommand()
        {
            Errors = "";
            var result = changePasswordValidator.Validate(this);
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
    }
}
