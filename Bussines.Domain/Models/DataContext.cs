
namespace Bussines.Domain.Models
{
    using System.Data.Entity;
    public class DataContext : DbContext
    {
        public DataContext():base("DefaultConnection")
        {

        }

        public DbSet<Common.Models.BandS> BandS { get; set; }

    }
}
