
namespace Bussines.ViewModels
{
    using System;
    using System.Linq;
    using System.Windows.Input;
    using Common.Models;
    using Helpers;
    using Views;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using Xamarin.Forms;
    
    public class BandSMasterItemViewModel :BandS
    {
        #region Attributes

        private ApiService apiService;
        #endregion

        #region Constructors
        
        public BandSMasterItemViewModel()
        {
            
            this.apiService = new ApiService();
        }
        #endregion


        #region Commands

        //public ICommand AddBusinessNoobCommand
        //{
        //    get
        //    {
        //        return new RelayCommand(GoToLogin);
        //    }
        //}

        //private async void GoToLogin()
        //{
        //    var connection = await this.apiService.CheckConnection();

        //    if (!connection.IsSuccess)
        //    {

        //        await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
        //        return;

        //    }

        //    MainViewModel.GetInstance().BandSMaster = new BandSPageMasterViewModel();
        //    await Application.Current.MainPage.Navigation.PushAsync(new BandSPageMaster());
        //}
        





        public ICommand EditProductCommand
        {
            get
            {
                return new RelayCommand(EditProduct);
            }
        }

        private async void EditProduct()
        {
            MainViewModel.GetInstance().EditBandS = new EditBandSViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new EditBandSPage());
        }

        public ICommand DeleteProductCommand
        {
            get
            {
                return new RelayCommand(DeleteProduct);
            }
        }

        private async void DeleteProduct()
        {
            var answer = await Application.Current.MainPage.DisplayAlert(
                "Confirmar",
                Languages.DeleteConfirmation,
                Languages.Yes,
                Languages.No);
            if (!answer)
            {
                return;
            }
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {

                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;

            }
            var url = Application.Current.Resources["UrlApi"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();
            var response = await this.apiService.Delete(url, prefix, controller, this.BandSId);
            if (!response.IsSuccess)
            {

                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            var bandSPageMasterViewModel = BandSPageMasterViewModel.GetInstance();
            var deleteBandS = bandSPageMasterViewModel.BandS.Where(p => p.BandSId == this.BandSId).FirstOrDefault();
            if (deleteBandS != null)
            {
                bandSPageMasterViewModel.BandS.Remove(deleteBandS);
            }
            //bandSPageMasterViewModel.RefreshList();
        }
        #endregion

    }
}
