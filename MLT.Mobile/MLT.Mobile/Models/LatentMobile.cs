using MLT.Mobile.Validators;
using MLT.Web.Contracts.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace MLT.Mobile.Models
{
    public class LatentMobile : INotifyPropertyChanged
    {
        private readonly LatentValidator latentValidator;
        

        public string Errors { get; set; }

        public LatentMobile()
        {
            this.latentValidator = new LatentValidator();
        }

        private bool isPalm;
        public bool IsPalm { get => isPalm; set { isPalm = value; OnPropertyChanged(nameof(IsPalm)); } }
        private string registrationNumber;
        public string RegistrationNumber { get => registrationNumber; set { registrationNumber = value.ToUpper(); OnPropertyChanged(nameof(RegistrationNumber)); } }
        private int? latentNumber;
        public int? LatentNumber { get => latentNumber; set { latentNumber = value; OnPropertyChanged(nameof(LatentNumber)); } }
        private DateTime crimeDate;
        public DateTime CrimeDate { get => crimeDate; set { crimeDate = value; OnPropertyChanged(nameof(CrimeDate)); } }
        private string crimePlace { get; set; }
        public string CrimePlace { get => crimePlace; set { crimePlace = value.ToUpper(); OnPropertyChanged(nameof(CrimePlace)); } }
        private string injuredLastnames;
        public string InjuredLastnames { get => injuredLastnames; set { injuredLastnames = value.ToUpper(); OnPropertyChanged(nameof(InjuredLastnames)); } }
        private string latentPlace;
        public string LatentPlace { get => latentPlace; set { latentPlace = value.ToUpper(); OnPropertyChanged(nameof(LatentPlace)); } }
        private string latentMethod;
        public string LatentMethod { get => latentMethod; set { latentMethod = value.ToUpper(); OnPropertyChanged(nameof(LatentMethod)); } }
        private string checkedLastnames;
        public string CheckedLastnames { get => checkedLastnames; set { checkedLastnames = value.ToUpper(); OnPropertyChanged(nameof(CheckedLastnames)); } }      
        private string imageBase64;
        public string ImageBase64 { get => imageBase64; set { imageBase64 = value; OnPropertyChanged(nameof(ImageBase64)); } }
        private IEnumerable<SingleClassifierWeb> entrancePlace;
        public IEnumerable<SingleClassifierWeb> EntrancePlace { get => entrancePlace; set { entrancePlace = value; OnPropertyChanged(nameof(EntrancePlace)); } }
        private IEnumerable<DualClassifierWeb> entranceType;
        public IEnumerable<DualClassifierWeb> EntranceType { get => entranceType; set { entranceType = value; OnPropertyChanged(nameof(EntranceType)); } }
        private IEnumerable<DualClassifierWeb> crimeType;
        public IEnumerable<DualClassifierWeb> CrimeType { get => crimeType; set { crimeType = value; OnPropertyChanged(nameof(CrimeType)); } }

        private Command latentCommand;
        public Command LatentCommand
        {
            get
            {
                return latentCommand ?? (latentCommand = new Command(ExecuteLatentCommand));
            }
        }

        public void ExecuteLatentCommand()
        {
            Errors = "";
            var result = latentValidator.Validate(this);
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

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
