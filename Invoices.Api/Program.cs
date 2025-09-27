using Invoices.Api;
using Invoices.Api.Interfaces;
using Invoices.Api.Managers;
using Invoices.Data;
using Invoices.Data.Entities;
using Invoices.Data.Entities.Enums;
using Invoices.Data.Interfaces;
using Invoices.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Invoices.Api", Version = "v1" });
});

// Controllers + JSON
builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Dependenci Injections
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IPersonManager, PersonManager>();
builder.Services.AddScoped<IInvoiceManager, InvoiceManager>();

// AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfile>());

// P�ipojen� k datab�zi, viz soubor appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Invoices.Api v1");
    });

    // Vytvo�en� vzorov�ch dat v p��pad�, �e neexistuj� ��dn� osoby (pro testovac� ��ely)
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Persons.Any())
    {
        db.Persons.AddRange(
            new Person
            {
                Name = "Jan Nov�k",
                IdentificationNumber = "12345678",
                TaxNumber = "CZ12345678",
                AccountNumber = "1234567890",
                BankCode = "0100",
                Iban = "CZ6501000000001234567890",
                Telephone = "+420123456789",
                Mail = "jan.novak@example.com",
                Street = "Ulice 1",
                Zip = "10000",
                City = "Praha",
                Country = Country.CZECHIA,
                Note = "Testovac� osoba",
                Hidden = false,
                Sales = new List<Invoice>(),
                Purchases = new List<Invoice>()
            },
            new Person
            {
                Name = "Anna Horv�thov�",
                IdentificationNumber = "87654321",
                TaxNumber = "SK87654321",
                AccountNumber = "0987654321",
                BankCode = "0900",
                Iban = "SK8909000000000987654321",
                Telephone = "+421987654321",
                Mail = "anna.horvathova@example.sk",
                Street = "Cesta 5",
                Zip = "81101",
                City = "Bratislava",
                Country = Country.SLOVAKIA,
                Note = "Druh� osoba",
                Hidden = false,
                Sales = new List<Invoice>(),
                Purchases = new List<Invoice>()
            }
        );

        db.SaveChanges();
    }
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
