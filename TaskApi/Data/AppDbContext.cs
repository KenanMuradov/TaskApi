using Microsoft.EntityFrameworkCore;

namespace TaskApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
