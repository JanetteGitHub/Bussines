using Bussines.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bussines.Backend.Models
{
    public class BandSView :BandS
    {
        public HttpPostedFileBase ImageFile { get; set; }
    }
}