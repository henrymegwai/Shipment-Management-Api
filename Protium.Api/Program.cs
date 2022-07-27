using Data;
using Data.Entities;
using Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>();
builder.Services.AddScoped<IRepository<Shipment>, ShipmentRepository>();
builder.Services.AddScoped<IRepository<Driver>, DriverRepository>();
using (var configuration = builder.Configuration)
{
    await DataGenerator.Initialize(configuration);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Protium Shipment Management Api v1";
    });
}

//app.UseAuthorization();

app.MapControllers();

app.Run();
