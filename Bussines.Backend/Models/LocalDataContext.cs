
namespace Bussines.Backend.Models
{
    using Bussines.Domain.Models;
    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<Bussines.Common.Models.BandS> BandS { get; set; }
    }
}