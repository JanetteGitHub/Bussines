
namespace Bussines.ViewModels
{
    using Bussines.Common.Models;
    using Bussines.Helpers;
    using Bussines.Services;
    using Bussines.Views;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class BandSViewModel : BaseViewModel
    {
        #region Attribb

        private string filter;
        private ApiService apiService;
        public bool isRefreshing;
        private ObservableCollection<BandSMasterItemViewModel> bandS;
        #endregion
        #region Properties
        public string Filter
        {
            get { return this.filter; }
            set
            {
                this.filter = value;
                this.RefreshList();
            }
        }
        public List<BandS> MyBandS { get; set; }
        public BandSPageMasterViewModel BandSMaster { get; set; }
        public ObservableCollection<BandSMasterItemViewModel> BandS
        {
            get { return this.bandS; }
            set { this.SetValue(ref this.bandS, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        #endregion
        #region Constructors
        public BandSViewModel()
        {
            instance = this;
            this.apiService = new ApiService();    
            this.LoadProducts();
        }
        #endregion
        #region Singleton
        private static BandSViewModel instance;
        public static BandSViewModel GetInstance()
        {
            if (instance == null)
            {
                return new BandSViewModel();
            }
            return instance;
        }

        #endregion

        #region Methods
        private async void LoadProducts()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;

            }

            var url = Application.Current.Resources["UrlApi"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();
            var response = await this.apiService.GetList<BandS>(url, prefix, controller);
            if (!response.IsSuccess)
            {
               this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);

                return;
            }

            this.MyBandS = (List<BandS>)response.Result;
            this.RefreshList();
            this.IsRefreshing = false;

        }
        public void RefreshList()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                var myListbandSMasterItemViewModel = this.MyBandS.Select(p => new BandSMasterItemViewModel
                {
                    Description = p.Description,
                    ImageArray = p.ImageArray,
                    ImagePath = p.ImagePath,
                    Phone = p.Phone,
                    BandSId = p.BandSId,
                    Address = p.Address,
                    Remarks = p.Remarks,
                    PublishOn = p.PublishOn,
                });

                this.BandS = new ObservableCollection<BandSMasterItemViewModel>(
                myListbandSMasterItemViewModel.OrderBy(p => p.Description));

            }
            else
            {
                var myListbandSMasterItemViewModel = this.MyBandS.Select(p => new BandSMasterItemViewModel
                {
                    Description = p.Description,
                    ImageArray = p.ImageArray,
                    ImagePath = p.ImagePath,
                    Phone = p.Phone,
                    BandSId = p.BandSId,
                    Address = p.Address,
                    Remarks = p.Remarks,
                    PublishOn = p.PublishOn,
                }).Where(p=> p.Description.ToLower().Contains(this.Filter.ToLower())).ToList();

                this.BandS = new ObservableCollection<BandSMasterItemViewModel>(
                myListbandSMasterItemViewModel.OrderBy(p => p.Description));
            }
            

           

        }

        #endregion
        #region Commands
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(RefreshList);
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadProducts);
            }
        }
        #endregion

    }
}
