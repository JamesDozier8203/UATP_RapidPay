using RapidPay.API.Business_Rules;
using RapidPay.API.Middleware;
using RapidPay.Domain;
using RapidPay.Repository;
using RapidPay.Repository.Helpers;

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

//add dependencies
services.AddScoped<ICreditCardRepository, CreditCardRepository>();
services.AddScoped<ITransactionRepository, TransactionRepository>();
services.AddScoped<ICardSecurity, CardSecurity>();
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
