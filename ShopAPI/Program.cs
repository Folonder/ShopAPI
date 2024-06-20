using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using ShopAPI.Data;
using ShopAPI.Mappers;
using ShopAPI.Middleware;
using ShopAPI.Repositories;
using ShopAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Since the application can run on both Windows and Linux, the type of path to the database will be different.
// In order for the docker to synchronize with the host, the database must be located in a folder.
var dbPath = Path.Combine("Database", "shop_api_database.db");
builder.Configuration["ConnectionStrings:SQLiteConnection"] = $"Data Source={dbPath}";

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddDbContext<ShopContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ShopContext>();

    if ((dbContext.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator)!.Exists() == false)
    {
        dbContext.Database.Migrate();
    }
}

app.UseSwagger();
app.UseSwaggerUI();


app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
