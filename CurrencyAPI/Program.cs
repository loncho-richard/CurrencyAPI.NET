using Microsoft.EntityFrameworkCore;
using Data;
using Data.Repository.Implementations;
using Data.Repository.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Services.Interfaces;
using Services.Implementation;
using Microsoft.AspNetCore.Authentication;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("CurrencyAPIBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Insert JWT, generated after being logged."
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "CurrencyAPIBearerAuth" }
                }, new List<string>() }
    });
});

builder.Services.AddDbContext<CurrencyAPIContext>(dbContextOptions => dbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:CurrencyAPIConnectionString"]));

builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();

builder.Services.AddScoped<ISubscriptionServices, SubscriptionServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ICurrencyServices, CurrencyServices>();
builder.Services.AddScoped<IConversionServices, ConversionServices>();
builder.Services.AddScoped<IVerifyToConversionServices, VerifyToConversionServices>();


builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]!))
        };
    }
);

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "AllowOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
