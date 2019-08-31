

namespace Bussines
{
    using Bussines.ViewModels;
    using Bussines.Views;
    using Xamarin.Forms;
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainViewModel.GetInstance().Welcome = new WelcomePageViewModel();
            MainPage = new WelcomePage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
