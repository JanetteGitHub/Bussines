
using Bussines.Common.Models;
using Bussines.Helpers;
using Bussines.Services;
using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bussines.ViewModels
{
    public class EditBandSViewModel : BaseViewModel
    {
        #region Attributs
        private MediaFile file;
        private ImageSource imageSource;
        private ApiService apiService;
        private bool isRunning;
        private bool isEnabled;
        private BandS bandS;
        #endregion

        #region Properties
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }
        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { this.SetValue(ref this.imageSource, value); }
        }

        public BandS BandS
        {
            get { return this.bandS; }
            set { this.SetValue(ref this.bandS, value); }
        }

        #endregion

        #region Constructors
        public EditBandSViewModel(BandS bandS)
        {
            this.bandS = bandS;
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.ImageSource = bandS.ImageFullPath;
        }
        #endregion
        #region Commands
        public ICommand changeImageCommand
        {
            get
            {
                return new RelayCommand(changeImage);
            }
        }

        private async void changeImage()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.ImageSource,
                Languages.Cancel,
                null,
                Languages.FromGallery,
                Languages.NewPicture);
            if (source == Languages.Cancel)
            {
                this.file = null;
                return;
            }
            if (source == Languages.NewPicture)
            {
                this.file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                   );
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync();
            }
            if (this.file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });
            }

        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.BandS.Description))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.DescriptionError,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.BandS.Address))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AddressError,
                    Languages.Accept);
                return;
            }
            //if (string.IsNullOrEmpty(this.BandS.Phone))
            //{
            //    await Application.Current.MainPage.DisplayAlert(
            //        Languages.Error,
            //        Languages.PhoneError,
            //        Languages.Accept);
            //    return;
            //}
            //if (string.IsNullOrEmpty(this.Remarks))
            //{
            //    await Application.Current.MainPage.DisplayAlert(
            //        Languages.Error,
            //        "Debe escribir informacion",
            //        Languages.Accept);
            //    return;
            //}

            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return;

            }

            byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FilesHelper.ReadFully(this.file.GetStream());
                this.BandS.ImageArray = imageArray;
            }

            var url = Application.Current.Resources["UrlApi"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();
            var response = await this.apiService.Put(url, prefix, controller,this.BandS,this.BandS.BandSId);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            var newBandS = (BandS)response.Result;
            var bandSPageMasterviewModel = BandSPageMasterViewModel.GetInstance();
            var oldBandS = bandSPageMasterviewModel.MyBandS.Where(p => p.BandSId == this.bandS.BandSId).FirstOrDefault();
            if( oldBandS != null)
            {
                bandSPageMasterviewModel.MyBandS.Remove(oldBandS);
            }

            bandSPageMasterviewModel.MyBandS.Add(newBandS);
            bandSPageMasterviewModel.RefreshList();

            this.IsRunning = false;
            this.IsEnabled = true;
            await Application.Current.MainPage.Navigation.PopAsync();





        }
        #endregion
    }
}
