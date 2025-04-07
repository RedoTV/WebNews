using WebNews.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5075")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add Redis cache
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "WebNews_";
});

// Add HttpClient
builder.Services.AddHttpClient();

// Add NewsService
builder.Services.AddScoped<INewsService, NewsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Use CORS
app.UseCors();

app.MapControllers();

app.Run();
