using DotnetBackend.Dao;
using DotnetBackend.Data;
using DotnetBackend.Services;
using FarmFresh.Data;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAdminDao, AdminDaoImpl>();
builder.Services.AddScoped<IStockDetailsRepository, StockDetailsRepository>();
builder.Services.AddScoped<IUserDao, UserDaoImpl>();
builder.Services.AddScoped<IFarmersDao, FarmersDaoImpl>();
builder.Services.AddScoped<IFarmersService, FarmersServiceImpl>();
builder.Services.AddScoped<IAdminService, AdminServiceImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMultipleOrigins",
        builder => builder
            .WithOrigins("http://localhost:3000", "https://farmer-market-place.vercel.app", "https://farmermarketplaceaspnetcore-production.up.railway.app", "https://farmermarketplaceasp-netcore.onrender.com/")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

builder.Services.AddDbContext<FarmersmarketContext>(options =>
{
    var connectionString = builder.Environment.IsDevelopment()
        ? builder.Configuration.GetConnectionString("Development")
        : builder.Configuration.GetConnectionString("Production");

    options.UseMySQL(connectionString!);
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();
}
else
{
    app.UseGlobalExceptionHandler();
}

app.UseForwardedHeaders();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An unexpected error occurred: {ex}");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "text/plain";

            await context.Response.WriteAsync("An unexpected error occurred.");
        }
    }
}

public static class GlobalExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}
