
namespace Bussines.ViewModels
{
    using Bussines.Helpers;
    using Bussines.Services;
    using Bussines.Views;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        #region Attributes

        private ApiService apiService;
        private bool isRunning;
        private bool isEnabled;

        #endregion
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsRemembered { get; set; }
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


        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
        }
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(GoBandSMasterPage);
            }

        }

        private async void GoBandSMasterPage()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "Debe ingresar un email",
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "Debe ingresar una contraseña",
                    Languages.Accept);
                return;
            }
            this.IsRunning = true;
            
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;

            }

            if (this.Email == "Yan@gmail.com".ToLower() && this.Password == "123456".ToLower())
            {
                MainViewModel.GetInstance().BandSMaster = new BandSPageMasterViewModel();
                Application.Current.MainPage = new NavigationPage(new BandSPageMaster());

            }
            else
            {
                this.IsRunning=false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    "correo o contraseña incorrectos",
                    Languages.Accept);
                return;
            }
            this.IsRunning = true;


        }
    }
}
