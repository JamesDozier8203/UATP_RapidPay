using Microsoft.Extensions.Configuration;
using RapidPay.API.Business_Rules;
using RapidPay.API.Middleware;
using RapidPay.Domain;
using RapidPay.Repository;
using RapidPay.Repository.Helpers;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RapidPay.API.Helpers;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

//database service
services.AddDbContext<DataContext>();
services.AddCors();


//add jwy authentication security
// configure strongly typed settings objects
var appSettingsSection = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

// configure jwt authentication
var key = Encoding.ASCII.GetBytes(appSettingsSection.Secret);
services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            var cardRepo = context.HttpContext.RequestServices.GetRequiredService<ICreditCardRepository>();
            var cardId = Guid.Parse(context.Principal.Identity.Name);
            var card = cardRepo.GetByIdAsync(cardId);
            if (card == null)
            {
                // return unauthorized if user no longer exists
                context.Fail("Unauthorized");
            }
            return Task.CompletedTask;
        }
    };
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//add dependencies
services.AddScoped<ICreditCardRepository, CreditCardRepository>();
services.AddScoped<ITransactionRepository, TransactionRepository>();
services.AddScoped<ICardSecurity, CardSecurity>();
services.AddSingleton<UFESingleton>();
services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// global cors policy
app.UseCors(x => x
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

// global error handler
app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
