using Controls.ImageCropper;
using MLT.Mobile.Helpers;
using MLT.Mobile.Models;
using MLT.Mobile.ServiceInterfaces;
using MLT.Web.Contracts.Helpers;
using MLT.Web.Contracts.WebModels;
using NativeMedia;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MLT.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LatentPage : ContentPage
    {
        private HttpInfo httpInfo;
        private LatentMobile latentMobile;
        private readonly IHttpService httpService;
        private string statusButtonClassId;
        private string cachePath = "/storage/emulated/0/Android/data/com.todes.mobilelatent/cache/";
        private string picturePath = "/storage/emulated/0/Pictures/mobilelatent.android/";

        public LatentPage(HttpInfo httpInfo)
        {
            InitializeComponent();
            this.crimeDatePicker.MaximumDate = DateTime.Now;

            this.httpInfo = httpInfo;
            this.httpService = DependencyService.Get<IHttpService>();
            latentMobile = new LatentMobile();
            latentMobile.CrimeDate = DateTime.Now;
            this.BindingContext = latentMobile;


        }

        private void typeLatentSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (typeLatentSwitch.IsToggled)
            {
                latentTypeLabel.Text = "След ладони";
            }
            else
            {
                latentTypeLabel.Text = "След пальца";
            }
        }

        private async Task<IEnumerable<string>> GetInformationList(string methodName, bool isDualClassifier)
        {
            httpInfo.ControllerName = "Information";
            httpInfo.MethodName = methodName;
            httpInfo.MethodType = RestSharp.Method.Get;
            httpInfo.JsonData = "";
            var stringResult = await httpService.CallHttpMethodAsync(httpInfo);

            IEnumerable<string> result;
            if (!isDualClassifier)
            {
                result = JsonConvert.DeserializeObject<IEnumerable<SingleClassifierWeb>>(stringResult).Select(p => p.Lex);
            }
            else
            {
                var temp = JsonConvert.DeserializeObject<IEnumerable<DualClassifierWeb>>(stringResult);//.Where(t => t.SecondLex == "\" \"").Select(p => p.Lex);
                result = temp.Where(t => t.Lex == " ").Select(p => p.SecondLex);
            }
            return result;
        }

        private async void addClassifier_clicked(object sender, EventArgs e)
        {
            mainStack.IsEnabled = false;
            statusButtonClassId = ((Button)sender).ClassId;
            IEnumerable<string> list = new List<string>();
            IEnumerable<string> selectedList = new List<string>();
            switch (statusButtonClassId)
            {
                case nameof(ButtonClassIdEnum.entrancePlaceButton):
                    {
                        list = await GetInformationList("GetEntrancePlaces", false);
                        selectedList = entrancePlaceEditor.Text?.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                    }
                    break;
                case nameof(ButtonClassIdEnum.entranceTypeButton):
                    {
                        list = await GetInformationList("GetEntranceTypes", true);
                        selectedList = entranceTypeEditor.Text?.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                    }
                    break;
                case nameof(ButtonClassIdEnum.crimeTypeButton):
                    {
                        list = await GetInformationList("GetCrimeTypes", true);
                        selectedList = crimeTypeEditor.Text?.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                    }
                    break;
            }

            var listViewPage = new ListViewPage(list, selectedList);
            listViewPage.ResultSucceeded += HandleEntrancePlace;
            await Navigation.PushModalAsync(listViewPage);
            
        }

        protected override void OnAppearing()
        {
            if (!mainStack.IsEnabled)
            {
                mainStack.IsEnabled = true;
            }
            base.OnAppearing();
        }

        private void HandleEntrancePlace(object sender, EventArgs e)
        {
            var text = "";
            var list = (IEnumerable<string>)sender;
            foreach (var current in list)
            {
                text += current + Environment.NewLine;
            }
            text = text.TrimEnd('\r', '\n');
            switch (statusButtonClassId)
            {
                case nameof(ButtonClassIdEnum.entrancePlaceButton):
                    {
                        entrancePlaceEditor.IsVisible = false;
                        entrancePlaceEditor.Text = text;
                        entrancePlaceEditor.IsVisible = !string.IsNullOrEmpty(text);
                        latentMobile.EntrancePlace = new List<SingleClassifierWeb>();
                        foreach (var current in list)
                        {
                            ((List<SingleClassifierWeb>)latentMobile.EntrancePlace).Add(
                                new SingleClassifierWeb()
                                {
                                    Lex = current
                                });
                        }                      
                    }
                    break;
                case nameof(ButtonClassIdEnum.entranceTypeButton):
                    {
                        entranceTypeEditor.IsVisible = false;
                        entranceTypeEditor.Text = text;
                        entranceTypeEditor.IsVisible = !string.IsNullOrEmpty(text);
                        latentMobile.EntranceType = new List<DualClassifierWeb>();
                        foreach (var current in list)
                        {
                            ((List<DualClassifierWeb>)latentMobile.EntranceType).Add(
                                new DualClassifierWeb()
                                {
                                    Lex = current
                                });
                        }
                    }
                    break;
                case nameof(ButtonClassIdEnum.crimeTypeButton):
                    {
                        crimeTypeEditor.IsVisible = false;
                        crimeTypeEditor.Text = text;
                        crimeTypeEditor.IsVisible = !string.IsNullOrEmpty(text);
                        latentMobile.CrimeType = new List<DualClassifierWeb>();
                        foreach (var current in list)
                        {
                            ((List<DualClassifierWeb>)latentMobile.CrimeType).Add(
                                new DualClassifierWeb()
                                {
                                    Lex = current
                                }) ;
                        }                        
                    }
                    break;
            }            
            mainStack.ResolveLayoutChanges();
        }

        private async Task<string> CropImage(string fullPath)
        {
            var result = await ImageCropper.Current.Crop(new CropSettings()
            {
                AspectRatioX = 1,
                AspectRatioY = 1,
                CropShape = CropSettings.CropShapeType.Rectangle
            }, fullPath);
            return result;
        }

        private string StreamToBase64(Stream stream)
        {
            var tempBuff = new byte[stream.Length];
            stream.Read(tempBuff, 0, (int)stream.Length);
            var result = Convert.ToBase64String(tempBuff);
            return result;
        }

        private async void getPhoto_clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {                    
                     Title = $"mlt.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss.ff")}"                    
                });   
                using (var stream = await photo.OpenReadAsync())
                {
                    await MediaGallery.SaveAsync(MediaFileType.Image, stream, photo.FileName);
                    latentMobile.ImageBase64 = StreamToBase64(stream);
                }
                imagePhoto.Source = ImageSource.FromFile(photo.FullPath);                
                mainStack.ResolveLayoutChanges();

                var croppedImagePath = await CropImage(photo.FullPath);
                if (!string.IsNullOrEmpty(croppedImagePath))
                {
                    FileResult file = new FileResult(croppedImagePath);
                    using (var stream = await file.OpenReadAsync())
                    {
                        latentMobile.ImageBase64 = StreamToBase64(stream);
                    }
                    imagePhoto.Source = ImageSource.FromFile(file.FullPath);                   
                    mainStack.ResolveLayoutChanges();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }

        private async void selectPhoto_clicked(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();                
                using (var stream = await photo.OpenReadAsync())
                {
                    await MediaGallery.SaveAsync(MediaFileType.Image, stream, $"temp_{photo.FileName}");
                    latentMobile.ImageBase64 = StreamToBase64(stream);
                }
                imagePhoto.Source = ImageSource.FromFile(photo.FullPath);
                mainStack.ResolveLayoutChanges();
                var croppedImagePath = await CropImage(photo.FullPath);
                if (!string.IsNullOrEmpty(croppedImagePath))
                {
                    var file = new FileResult(croppedImagePath);
                    using (var stream = await file.OpenReadAsync())
                    {
                        latentMobile.ImageBase64 = StreamToBase64(stream);    
                    }
                    imagePhoto.Source = ImageSource.FromFile(file.FullPath);
                    mainStack.ResolveLayoutChanges();
                }
                try
                {
                    if (Directory.Exists(cachePath))
                    {
                        Directory.Delete(cachePath, true);
                    }
                    if (Directory.Exists(picturePath))
                    {
                        string[] fileList = Directory.GetFiles(picturePath, @"temp_*.jpg");
                        foreach (var file in fileList)
                        {
                            File.Delete(file);
                        }
                    }                    
                }
                catch (Exception) { }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }

        private void LockComponents()
        {
            mainStack.IsVisible = false;
            activityStack.IsVisible = true;
        }

        private void UnlockComponents()
        {
            mainStack.IsVisible = true;
            activityStack.IsVisible = false;
        }

        private async void sendLatent_clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(latentMobile.Errors))
            {
                LockComponents();
                try
                {
                    var latentWeb = new LatentWeb()
                    {
                        CheckedLastnames = latentMobile.CheckedLastnames,
                        CrimeDate = latentMobile.CrimeDate,
                        CrimePlace = latentMobile.CrimePlace,
                        CrimeType = latentMobile.CrimeType,
                        EntrancePlace = latentMobile.EntrancePlace,
                        EntranceType = latentMobile.EntranceType,
                        ImageBase64 = latentMobile.ImageBase64,
                        InjuredLastnames = latentMobile.InjuredLastnames,
                        IsPalm = latentMobile.IsPalm,
                        LatentMethod = latentMobile.LatentMethod,
                        LatentNumber = latentMobile.LatentNumber ??= 1,
                        LatentPlace = latentMobile.LatentPlace,
                        RegistrationNumber = latentMobile.RegistrationNumber
                    };
                    httpInfo.ControllerName = "Latent";
                    httpInfo.MethodName = "SendLatent";
                    httpInfo.MethodType = RestSharp.Method.Post;
                    httpInfo.JsonData = JsonConvert.SerializeObject(latentWeb);
                    var stringResult = await httpService.CallHttpMethodAsync(httpInfo);
                    if (stringResult == "OK")
                    {
                        await DisplayAlert("Success", "След был добавлен", "Ok");
                        latentMobile.LatentNumber = null;
                        latentMobile.ImageBase64 = null;
                        this.imagePhoto.Source = null;
                        
                    }
                    else
                    {
                        await DisplayAlert("Error", stringResult, "Ok");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.ToString(), "Ok");
                }
            }
            else
            {
                await DisplayAlert("Validatation", latentMobile.Errors, "Ok");
            }
            UnlockComponents();
            mainStack.ResolveLayoutChanges();
        }

        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            this.ForceLayout();
        }

        private void latentNumberEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {                
                latentMobile.LatentNumber = null;
                return;
            }

            if (!int.TryParse(e.NewTextValue, out int value))
            {
                ((Entry)sender).Text = e.OldTextValue;
            }
        }
    }
}