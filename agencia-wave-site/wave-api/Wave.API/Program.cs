using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wave.Application.Services;
using Wave.Application.Services.Interfaces;
using Wave.Domain.Entities;
using Wave.Infra;
using Wave.Infra.Data.Context;
using Wave.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddInfrastructure(builder.Configuration);


builder.Services.AddIdentityCore<Usuario>()
		.AddUserStore<UsuarioRepository>()
		.AddDefaultTokenProviders()
		.AddSignInManager<SignInManager<Usuario>>();

builder.Services.AddScoped<UserManager<Usuario>>();

builder.Services.AddAuthentication("Bearer").AddJwtBearer();
builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
