using Microsoft.EntityFrameworkCore;
using PolymerSamples.Data;
using PolymerSamples.Interfaces;
using PolymerSamples.Repository;
using PolymerSamples.Authorization;
using PolymerSamples.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNet.Identity;
using Npgsql;
using Microsoft.OpenApi.Models;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Parse("192.168.1.54"), 5000);
});

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddScoped<ICodeRepository, CodeRepository>();
builder.Services.AddScoped<IVaultRepository, VaultRepository>();
builder.Services.AddScoped<ICodeVaultRepository, CodeVaultRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{ 
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ClockSkew = new TimeSpan(0, 0, 5),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:SecretKey"]))
                    };
                });

builder.Services.AddAuthorization(options =>
{// Эта конфигурация на данный момент бесполезна тк я использую кастомный RequiresCalimAttribute вместо Политик
    options.AddPolicy(AuthData.AdminPolicyName, policy =>
    {
        policy.RequireClaim(AuthData.RoleClaimType, AuthData.AdminClaimValue);
    });
    options.AddPolicy(AuthData.ViwerPolicyName, policy =>
    {
        policy.RequireClaim(AuthData.RoleClaimType, AuthData.ViewerClaimValue);
    });
    options.AddPolicy(AuthData.EditorPolicyName, policy =>
    {
        policy.RequireClaim(AuthData.RoleClaimType, [AuthData.EditorClaimValue, AuthData.AdminClaimValue]);
    });
});

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"));
    options.EnableSensitiveDataLogging(true);
    options.UseSnakeCaseNamingConvention();
});
// CORS setup
builder.Services.AddCors(options =>
{
    options.AddPolicy("defaultPolicy", policyBuilder =>
    {
        policyBuilder.WithOrigins(builder.Configuration.GetSection("CorsOptions:Origins").Get<string[]>());
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
        policyBuilder.AllowCredentials();
    });
});

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
app.UseSwaggerUI();
}
app.UseCors("defaultPolicy");

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

