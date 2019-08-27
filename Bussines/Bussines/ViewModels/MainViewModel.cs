
namespace Bussines.ViewModels
{
    public class MainViewModel
    {
        public BandSViewModel BandS { get; set; }
        public MainViewModel()
        {
            this.BandS = new BandSViewModel();
        }
    }
}
