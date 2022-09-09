using BusinessLogic.Data;
using BusinessLogic.Logic;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ITokenService, TokenService>();

// For Identity
builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<SeguridadDbContext>()
    .AddRoles<IdentityRole>()
    .AddSignInManager<SignInManager<Usuario>>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"])),
        ValidIssuer = builder.Configuration["Token:Issuer"],

        ValidateIssuer = true,
        //Lo activo si no deseo que cualquiera vea acceda a mis endpoint
        ValidateAudience = false
    };

});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddDbContext<ApiDisneyDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddDbContext<SeguridadDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("IdentitySeguridad"));
});

builder.Services.AddAutoMapper(typeof(PeliculaRepository));
builder.Services.AddTransient<IPeliculaRepository, PeliculaRepository>();
builder.Services.AddTransient<IPersonajeRepository, PersonajeRepository>();
builder.Services.AddSingleton<ISendGridEnviar, SendGridEnviar>();


builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve)
    .AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var loggerFactory = scope.ServiceProvider.GetService<ILoggerFactory>();

    try
    {
        var context = service.GetRequiredService<ApiDisneyDbContext>();
        context.Database.Migrate();

        var userManager = service.GetRequiredService<UserManager<Usuario>>();
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
        var identityContext = service.GetRequiredService<SeguridadDbContext>();
        await identityContext.Database.MigrateAsync();
        await SeguridadDbContextData.SeedUserAsync(userManager, roleManager);
    }
    catch (Exception e)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(e, "Error en el Proceso de Migración");
    }
}

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
