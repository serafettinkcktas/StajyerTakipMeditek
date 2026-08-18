using System.Text;
using Application.Common.Helpers;
using Application.Common.Models;
using Application.Common.Services;
using Application.Interface;
using Application.UseCases.Admin;
using Application.UseCases.Auth;
using Application.Validation.Auth;
using Application.Validation.Intern;
using Application.Validation.Mentor;
using Domain.Interface;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Persistence;
using Infrastructure.Repository;
using Infrastructure.Seed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// JWT Options
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SectionName));
var jwtOptions = builder.Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()
    ?? throw new InvalidOperationException("JWT configuration is missing");
builder.Services.AddSingleton(jwtOptions);

// Persistence
builder.Services.AddSingleton<IDbConnectionFactory, SqlConnectionHandler>();

// Repositories
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IInternStatusRepository, InternStatusRepository>();
builder.Services.AddScoped<IMentorRepository, MentorRepository>();
builder.Services.AddScoped<IInternRepository, InternRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

// Helpers
builder.Services.AddScoped<AccountHelper>();
builder.Services.AddScoped<UserProfileHelper>();
builder.Services.AddScoped<MentorHelper>();
builder.Services.AddScoped<InternHelper>();

// Services
builder.Services.AddScoped<ITokenService, TokenService>();

// UseCases
builder.Services.AddScoped<CreateRoleUseCase>();
builder.Services.AddScoped<AddMentorUseCase>();
builder.Services.AddScoped<AddInternUseCase>();
builder.Services.AddScoped<GetMentorsUseCase>();
builder.Services.AddScoped<GetInternsUseCase>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<RefreshTokenUseCase>();
builder.Services.AddScoped<LogoutUseCase>();

// Seed
builder.Services.AddScoped<RoleSeed>();
builder.Services.AddScoped<InternStatusSeed>();
builder.Services.AddScoped<AdminSeed>();

// Validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<AddMentorValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AddInternValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();

// Auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddAuthorization();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Temel rolleri ve stajyer durumlarını seed et
using (var scope = app.Services.CreateScope())
{
    var roleSeed = scope.ServiceProvider.GetRequiredService<RoleSeed>();
    await roleSeed.SeedAsync();

    var internStatusSeed = scope.ServiceProvider.GetRequiredService<InternStatusSeed>();
    await internStatusSeed.SeedAsync();

    var adminSeed = scope.ServiceProvider.GetRequiredService<AdminSeed>();
    await adminSeed.SeedAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAll");
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();