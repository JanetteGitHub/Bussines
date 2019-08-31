
namespace Bussines.ViewModels
{
    using System.Windows.Input;
    using Services;
    using Bussines.Helpers;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;
    using Bussines.Common.Models;
    using System.Linq;
    using Plugin.Media.Abstractions;
    using Plugin.Media;

    public class AddBandSPageViewModel : BaseViewModel
    {
        #region Attributes
        private MediaFile file;
        private ImageSource imageSource;
        private ApiService apiService;
        private bool isRunning;
        private bool isEnabled;

        #endregion
        #region Properties
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Remarks { get; set; }
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
        #endregion
        #region Constructors
        public AddBandSPageViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.imageSource = "business";
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
            if (string.IsNullOrEmpty(this.Description))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.DescriptionError,
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.Address))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.AddressError,
                    Languages.Accept);
                return;
            }          

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
            }


            var bandS = new BandS
            {
                Description=this.Description,
                Phone=this.Phone,
                Address=this.Address,
                Remarks=this.Remarks,
                ImageArray=imageArray,
            };

            var url = Application.Current.Resources["UrlApi"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();
            var response = await this.apiService.Post(url, prefix, controller,bandS);

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
            bandSPageMasterviewModel.MyBandS.Add(newBandS);
            bandSPageMasterviewModel.RefreshList();

            this.IsRunning = false;
            this.IsEnabled = true;
            await Application.Current.MainPage.Navigation.PopAsync();



            

        }
        #endregion
    }
}
