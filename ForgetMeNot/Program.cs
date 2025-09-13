using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using ForgetMeNot.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Prod secret setup
//if (builder.Environment.IsProduction())
//{
//    var kvUri = new Uri(builder.Configuration["KeyVault:Uri"]!);
//    var client = new SecretClient(kvUri, new DefaultAzureCredential());
//    KeyVaultSecret secret = await client.GetSecretAsync("JwtSigningKey");
//    builder.Configuration["Jwt:SigningKey"] = secret.Value;
//}

var signingKeyValue = builder.Configuration["Jwt:SigningKey"];
if (string.IsNullOrWhiteSpace(signingKeyValue))
{
    throw new InvalidOperationException("Missing configuration: Jwt:SigningKey");
}

var signingKeyByteArray = Encoding.UTF8.GetBytes(signingKeyValue);
var signingKey = new SymmetricSecurityKey(signingKeyByteArray);

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer((Action<JwtBearerOptions>)(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // issuer
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],

        // audience
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],

        // key
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = signingKey,

        ValidateLifetime = true,
    };
}));
builder.Services.AddAuthorization();


// EF Core
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by a space and your token. Example: Bearer abc123"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

// Run migrations on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();