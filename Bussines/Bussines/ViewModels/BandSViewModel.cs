
namespace Bussines.ViewModels
{
    using Bussines.Common.Models;
    using Bussines.Helpers;
    using Bussines.Services;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class BandSViewModel : BaseViewModel
    {
        #region Attribb
        private ApiService apiService;
        public bool isRefreshing;
        
        private ObservableCollection<BandS> bandS;
        #endregion
        #region Properties
        public ObservableCollection<BandS> BandS
        {
            get { return this.bandS; }
            set { this.SetValue(ref this.bandS, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }
        public BandSViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }
        #endregion

        private async void LoadProducts()
        {
            IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error,connection.Message, Languages.Accept);
                return;

            }

            var url = Application.Current.Resources["UrlApi"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();
            var response = await this.apiService.GetList<BandS>(url, prefix, controller);
            if (!response.IsSuccess)
            {
                IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);

                return;
            }

            var list = (List<BandS>)response.Result;
            this.BandS = new ObservableCollection<BandS>(list);
            IsRefreshing = false;
        }

            public ICommand RefreshCommand
            {
                get
                {
                    return new RelayCommand(LoadProducts);
                }
            }
        
    }
}
