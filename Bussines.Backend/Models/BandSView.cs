
namespace Bussines.Backend.Models
{
    using Bussines.Common.Models;
    using System.Web;
    public class BandSView :BandS
    {
        public HttpPostedFileBase ImageFile { get; set; }
    }
}