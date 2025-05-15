
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wave.Application.Services;
using Wave.Application.Services.Interfaces;
using Wave.Domain.Repositories;
using Wave.Domain.Repository;
using Wave.Infra.Data.Context;
using Wave.Infra.Repositories;

namespace Wave.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var teste = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<WaveDbContext>(options =>
            {
                options.UseSqlServer(teste);
            });

            #region [ Repositories ]
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IAssinaturaRepository, AssinaturaRepository>();
            services.AddScoped<IItemGaleriaRepository, ItemGaleriaRepository>();
            services.AddScoped<IFavoritoRepository, FavoritoRepository>();
			#endregion

			#region [ Services ]
			services.AddScoped<IPessoaService, PessoaService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IAssinaturaService, AssinaturaService>();
            services.AddScoped<IGaleriaService, GaleriaService>();
            services.AddScoped<IFavoritoService, FavoritoService>();
            #endregion

            return services;
        }
    }
}
