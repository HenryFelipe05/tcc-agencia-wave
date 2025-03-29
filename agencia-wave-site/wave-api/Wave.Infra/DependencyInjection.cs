﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wave.Application.Services;
using Wave.Application.Services.Interfaces;
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
			#endregion

			#region [ Services ]
			services.AddScoped<IPessoaService, PessoaService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            #endregion

            return services;
        }
    }
}
