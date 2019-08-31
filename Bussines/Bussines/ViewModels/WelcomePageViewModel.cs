
namespace Bussines.ViewModels
{

    using Bussines.Helpers;
    using Bussines.Services;
    using Bussines.Views;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;


    public class WelcomePageViewModel : BaseViewModel
    {

        #region Attributes

        private ApiService apiService;
        private bool isRunning;
        private bool isEnabled;

        #endregion

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

        public WelcomePageViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
        }

        public ICommand WelcomeCommand
        {
            get
            {
                return new RelayCommand(GoBandSPage);
            }

        }

        private async void GoBandSPage()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;

            }
  
            MainViewModel.GetInstance().BandS = new BandSViewModel();
            Application.Current.MainPage = new NavigationPage(new BandSPage());
            
            this.IsRunning = true;
            this.IsEnabled = false;
        }
    }
}
