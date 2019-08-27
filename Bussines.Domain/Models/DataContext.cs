
namespace Bussines.Domain.Models
{
    using System.Data.Entity;
    public class DataContext : DbContext
    {
        public DataContext():base("DefaultConnection")
        {

        }

        public System.Data.Entity.DbSet<Bussines.Common.Models.BandS> BandS { get; set; }
    }
}
