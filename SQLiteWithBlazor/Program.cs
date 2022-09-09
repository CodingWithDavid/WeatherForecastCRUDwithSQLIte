using Blazored.Toast;
using Microsoft.EntityFrameworkCore;

//locals
using SQLiteWithBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


builder.Services.AddDbContext<WeatherDbContext>(options =>
{
    options.UseSqlite("Data Source = data\\WeatherData.db");
});

builder.Services.AddScoped<WeatherForecastService>();
builder.Services.AddScoped<AppDataService>();
builder.Services.AddBlazoredToast();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
