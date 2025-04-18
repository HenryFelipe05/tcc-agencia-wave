﻿using Microsoft.EntityFrameworkCore;
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

        public virtual DbSet<Genero> Generos { get; set; }
        public virtual DbSet<Perfil> Perfis { get; set; }
        public virtual DbSet<Pessoa> Pessoas { get; set; }
        public virtual DbSet<TipoPessoa> TiposPessoa { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

		public DbConnection GetDbConnection()
		{
			return Database.GetDbConnection();
		}
	}
}
