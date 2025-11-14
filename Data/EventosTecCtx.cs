using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class WebAppCtx : DbContext
    {
        public WebAppCtx(DbContextOptions<WebAppCtx> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
