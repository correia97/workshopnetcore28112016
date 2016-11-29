using Microsoft.EntityFrameworkCore;

namespace workshopcore20161128.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        :base(options)
        {
            
        }
        public DbSet<Pessoa> Pessoas { get; set; }

    }
}