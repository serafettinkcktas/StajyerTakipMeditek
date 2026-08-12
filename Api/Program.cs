using Application.Common.Helpers;
using Application.UseCases.Admin;
using Application.Validation.Mentor;
using Domain.Interface;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Persistence;
using Infrastructure.Repository;
using Infrastructure.Seed;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Persistence
builder.Services.AddSingleton<IDbConnectionFactory, SqlConnectionHandler>();

// Repositories
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

// Helpers
builder.Services.AddScoped<AccountHelper>();
builder.Services.AddScoped<UserProfileHelper>();

builder.Services.AddScoped<MentorHelper>();

// UseCases
builder.Services.AddScoped<CreateRoleUseCase>();
builder.Services.AddScoped<AddMentorUseCase>();

// Seed
builder.Services.AddScoped<RoleSeed>();

// Validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<AddMentorValidator>();

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

// Temel rolleri seed et (Admin, Mentor, Stajyer)
using (var scope = app.Services.CreateScope())
{
    var roleSeed = scope.ServiceProvider.GetRequiredService<RoleSeed>();
    await roleSeed.SeedAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAll");
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();