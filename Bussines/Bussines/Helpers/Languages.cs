
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
    }
}
