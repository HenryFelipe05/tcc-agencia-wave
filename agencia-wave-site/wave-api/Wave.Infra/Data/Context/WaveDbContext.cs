using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Wave.Domain.Entities;

namespace Wave.Infra.Data.Context
{
    public partial class WaveDbContext : DbContext
    {
        public WaveDbContext(DbContextOptions<WaveDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura o tipo da coluna 'Arquivo' para VARBINARY(MAX)
            modelBuilder.Entity<ItemGaleria>()
                .Property(i => i.Arquivo)
                .HasColumnType("VARBINARY(MAX)");
        }

        public virtual DbSet<Genero> Generos { get; set; }
        public virtual DbSet<Perfil> Perfis { get; set; }
        public virtual DbSet<Pessoa> Pessoas { get; set; }
        public virtual DbSet<TipoPessoa> TiposPessoa { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Assinatura> Assinaturas { get; set; }
        public virtual DbSet<TipoAssinatura> TiposAssinatura { get; set; }
        public virtual DbSet<StatusAssinatura> StatusAssinaturas { get; set; }
        public virtual DbSet<ItemGaleria> ItemGalerias { get; set; }
        public virtual DbSet<Favorito> Favoritos { get; set; }

        public DbConnection GetDbConnection()
		{
			return Database.GetDbConnection();
		}
	}
}
