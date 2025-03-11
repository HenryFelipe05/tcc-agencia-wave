using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wave.Application.Interfaces.Repository;
using Wave.Application.Services;
using Wave.Application.Services.Interfaces;
using Wave.Infra.Data.Context;
using Wave.Infra.Repositories;

namespace Wave.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WaveDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            #region [ Repositories ]
            services.AddScoped<IPessoaRepository, PessoaRepository>();
			#endregion

			#region [ Services ]
			services.AddScoped<IPessoaService, PessoaService>();
			#endregion
         
			return services;
        }
    }
}
