using Data;
using Data.Entities;
using Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>();
builder.Services.AddScoped<IRepository<Shipment>, ShipmentRepository>();
builder.Services.AddScoped<IRepository<Driver>, DriverRepository>();
using (var configuration = builder.Configuration)
{
    await DataGenerator.Initialize(configuration);
}
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
