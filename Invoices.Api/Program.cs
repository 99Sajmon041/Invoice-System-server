using Invoices.Data.Repositories;
using System.Text.Json;
using Invoices.Api;
using Invoices.Api.Managers;
using Invoices.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Invoices.Data.Entities.Enums;
using Invoices.Data.Entities;
using System.Reflection;
using Invoices.Api.Interfaces;
using Invoices.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

// Swagger = nástroj pro dokumentaci a testování API
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // JSON vlastnosti budou camelCase (napø. "firstName" místo "FirstName")
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        // Enumy budou serializovány jako øetìzce
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonManager, PersonManager>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperProfile>();
});

// Pøipojení k databázi, viz soubor appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();

    // Vytvoøení vzorových dat v pøípadì, že neexistují žádné osoby (pro testovací úèely)
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!db.Persons.Any())
        {
            db.Persons.AddRange(
                new Person
                {
                    Name = "Jan Novák",
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
                    Note = "Testovací osoba",
                    Hidden = false
                },
                new Person
                {
                    Name = "Anna Horváthová",
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
                    Note = "Druhá osoba",
                    Hidden = false
                }
            );

            db.SaveChanges();
        }
    }
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
