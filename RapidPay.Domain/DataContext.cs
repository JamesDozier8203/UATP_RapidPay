namespace RapidPay.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class DataContext : DbContext
{
    protected readonly IConfigurationRoot configuration;

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        configuration = new ConfigurationBuilder()
        .SetBasePath(System.AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json")
        .Build();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlServer
        (
            configuration.GetConnectionString("RapidPay_Database"),
            m => m.MigrationsAssembly("RapidPay.API")
        );

    public DbSet<CreditCard> CreditCard { get; set; }
    public DbSet<Transaction> Transaction { get; set; }

}

