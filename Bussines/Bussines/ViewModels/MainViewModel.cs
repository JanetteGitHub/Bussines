
using Bussines.Services;
using Bussines.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bussines.ViewModels
{
    public class MainViewModel
    {

        #region Properties
        public WelcomePageViewModel Welcome { get; set; }
        public EditBandSViewModel EditBandS { get; set; }
        public BandSViewModel BandS { get; set; }
        public BandSPageMasterViewModel BandSMaster { get; set; }
        public AddBandSPageViewModel AddBandS { get; set; }
        public LoginViewModel Login { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            //this.Welcome = new WelcomePageViewModel();  
        } 
        #endregion

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }

        #endregion


        public ICommand AddBusinessNoobCommand
        {
            get
            {
                return new RelayCommand(GoToLogin);
            }
        }

        private async void GoToLogin()
        {
            this.Login = new LoginViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }


        public ICommand AddBusinessCommand
        {
            get
            {
                return new RelayCommand(GoToAdd);
            }
        }

        private async void GoToAdd()
        {
            this.AddBandS = new AddBandSPageViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AddBandSPage());
        }
    }
}
