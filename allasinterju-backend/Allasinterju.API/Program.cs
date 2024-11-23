using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Allasinterju.Database.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.AddDbContext<AllasinterjuContext>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICompetenceService, CompetenceService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>{
            var token = context.HttpContext.Request.Cookies["JWT_TOKEN"];
            if (!string.IsNullOrEmpty(token))
            {
                context.Token = $"{token}";
                
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes("oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo");

                try{
                    var principal = tokenHandler.ValidateToken(context.Token, new TokenValidationParameters{
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidIssuer = "hu.jobhub",
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero,
                        RoleClaimType = ClaimTypes.Role,
                        NameClaimType = ClaimTypes.Name                    
                    }, out SecurityToken validatedToken);

                    context.HttpContext.User = principal;
                    var idClaim = context.Principal.Claims.FirstOrDefault(c => c.Type == "id");
                }
                catch (Exception ex){                    
                    Console.WriteLine($"Token validation failed: {ex.Message}");
                    context.NoResult();
                }
            }
            
            return Task.CompletedTask;
        },
        OnChallenge = context => {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/json";
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                Path = "/"
            };

            context.Response.Cookies.Append("JWT_TOKEN", string.Empty, cookieOptions);
            context.Response.Cookies.Append("JWT_SIG", string.Empty, cookieOptions);
            context.HttpContext.User=null;
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context => {
            Console.WriteLine("Authentication failed: " + context.Exception);
            return Task.CompletedTask;
        }
        /*OnTokenValidated = context =>{
            var identity = context.Principal.Identity as ClaimsIdentity;
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, identity.FindFirst("nameid").Value));
            return Task.CompletedTask;
        }*/
    };
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "hu.pe.mik.tetelhuzo",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo")),
        RoleClaimType = ClaimTypes.Role,
        NameClaimType = ClaimTypes.Role,        
        ClockSkew=TimeSpan.Zero
    };
    options.MapInboundClaims = false;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(options => {
    options.WithOrigins("http://localhost:4200", "https://localhost:4200")
           .AllowAnyHeader()
           .AllowAnyMethod()
           .AllowCredentials();
});

app.Run();
