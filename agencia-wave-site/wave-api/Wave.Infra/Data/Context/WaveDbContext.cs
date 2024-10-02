using Microsoft.EntityFrameworkCore;

namespace Wave.Infra.Data.Context
{
    public partial class WaveDbContext : DbContext
    {
        public WaveDbContext(DbContextOptions<WaveDbContext> options)
            : base(options)
        {
        }

       // public virtual DbSet<>  { get; set; }
    }
}
