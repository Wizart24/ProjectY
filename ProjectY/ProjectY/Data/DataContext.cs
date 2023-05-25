using Microsoft.EntityFrameworkCore;
using ProjectY.Models;

namespace ProjectY.Data
{
	public class DataContext : DbContext
	{
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Picture> Pictures { get; set; }
    }
}
