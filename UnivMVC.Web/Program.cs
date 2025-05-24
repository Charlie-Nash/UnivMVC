using Microsoft.Extensions.Options;
using UnivMVC.Application.Config;
using UnivMVC.Application.Interfaces;
using UnivMVC.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiSrv01>(builder.Configuration.GetSection("ApiSrv01"));
builder.Services.Configure<ApiSrv02>(builder.Configuration.GetSection("ApiSrv02"));
builder.Services.Configure<ApiSrv03>(builder.Configuration.GetSection("ApiSrv03"));

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpClient<ILoginService, LoginService>((serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<ApiSrv01>>().Value;

    client.BaseAddress = new Uri(settings.BaseUrl);
    client.DefaultRequestHeaders.Add(settings.AppKeyName, settings.AppKeyValue);
});

builder.Services.AddHttpClient<IAcademicService, AcademicService>((serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<ApiSrv02>>().Value;

    client.BaseAddress = new Uri(settings.BaseUrl);
    client.DefaultRequestHeaders.Add(settings.AppKeyName, settings.AppKeyValue);
});

builder.Services.AddHttpClient<ICtaCteService, CtaCteService>((serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<ApiSrv03>>().Value;

    client.BaseAddress = new Uri(settings.BaseUrl);
    client.DefaultRequestHeaders.Add(settings.AppKeyName, settings.AppKeyValue);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");

app.Run();