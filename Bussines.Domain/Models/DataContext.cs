
namespace Bussines.Domain.Models
{
    using Bussines.Common.Models;
    using System.Data.Entity;
    public class DataContext : DbContext
    {
        public DataContext():base("DefaultConnection")
        {

        }

        public DbSet<BandS> BandS { get; set; }

    }
}
