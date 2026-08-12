using Application.Common.Helpers;
using Application.Interface;
using Application.UseCases.Admin;
using Application.Validation.Intern;
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
builder.Services.AddScoped<IInternStatusRepository, InternStatusRepository>();
builder.Services.AddScoped<IMentorRepository, MentorRepository>();

// Helpers
builder.Services.AddScoped<AccountHelper>();
builder.Services.AddScoped<UserProfileHelper>();
builder.Services.AddScoped<MentorHelper>();
builder.Services.AddScoped<InternHelper>();

// UseCases
builder.Services.AddScoped<CreateRoleUseCase>();
builder.Services.AddScoped<AddMentorUseCase>();
builder.Services.AddScoped<AddInternUseCase>();
builder.Services.AddScoped<GetMentorsUseCase>();

// Seed
builder.Services.AddScoped<RoleSeed>();
builder.Services.AddScoped<InternStatusSeed>();

// Validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<AddMentorValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AddInternValidator>();

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