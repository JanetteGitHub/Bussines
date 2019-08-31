
namespace Bussines.Helpers
{
    using Bussines.Interfaces;
    using Bussines.Resources;
    using Xamarin.Forms;

    public class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCulturenInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }
        public static string Accept
        {
            get { return Resource.Accept; }
        }
        public static string Error
        {
            get { return Resource.Error; }
        }
        public static string TurnOnInternet
        {
            get { return Resource.TurnOnInternet; }
        }
        public static string NoInternet
        {
            get { return Resource.NoInternet; }
        }
        public static string Login
        {
            get { return Resource.Login; }
        }
        public static string Welcome
        {
            get { return Resource.Welcome; }
        }
        public static string BandS
        {
            get { return Resource.BandS; }
        }
        public static string phone
        {
            get { return Resource.phone; }
        }
        public static string PhonePlaceHolder
        {
            get { return Resource.PhonePlaceHolder; }
        }
        public static string Address
        {
            get { return Resource.Address; }
        }
        public static string AddressPlaceHolder
        {
            get { return Resource.AddressPlaceHolder; }
        }
        public static string Remarks
        {
            get { return Resource.Remarks; }
        }
        public static string Image
        {
            get { return Resource.Image; }
        }
        public static string RemarksPlaceHolder
        {
            get { return Resource.RemarksPlaceHolder; }
        }
        public static string Save
        {
            get { return Resource.Save; }
        }
        public static string Delete
        {
            get { return Resource.Delete; }
        }
        public static string Edit
        {
            get { return Resource.Edit; }
        }
        public static string ChangeImage
        {
            get { return Resource.ChangeImage; }
        }
        public static string Description
        {
            get { return Resource.Description; }
        }
        public static string DescriptionPlaceHolder
        {
            get { return Resource.DescriptionPlaceHolder; }
        }
        public static string AddressError
        {
            get { return Resource.AddressError; }
        }
        public static string DescriptionError
        {
            get { return Resource.DescriptionError; }
        }
        public static string PhoneError
        {
            get { return Resource.PhoneError; }
        }
        public static string ImageSource
        {
            get { return Resource.ImageSource; }
        }
        public static string FromGallery
        {
            get { return Resource.FromGallery; }
        }
        public static string NewPicture
        {
            get { return Resource.NewPicture; }
        }
        public static string Cancel
        {
            get { return Resource.Cancel; }
        }
        public static string Yes
        {
            get { return Resource.Yes; }
        }
        public static string DeleteConfirmation
        {
            get { return Resource.DeleteConfirmation; }
        }
        public static string No
        {
            get { return Resource.No; }
        }
        public static string EditBandS
        {
            get { return Resource.EditBandS; }
        }
        public static string Email
        {
            get { return Resource.Email; }
        }
        public static string EmailPlaceHolder
        {
            get { return Resource.EmailPlaceHolder; }
        }
        public static string Password
        {
            get { return Resource.Password; }
        }
        public static string PasswordPlaceHolder
        {
            get { return Resource.PasswordPlaceHolder; }
        }
        public static string Rememberme
        {
            get { return Resource.Rememberme; }
        }
        public static string Forgot
        {
            get { return Resource.Forgot; }
        }
        public static string Register
        {
            get { return Resource.Register; }
        }

    }
}
