using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjetoLocadoraDeVeiculos.Data;
using ProjetoLocadoraDeVeiculos.Helper;
using ProjetoLocadoraDeVeiculos.Repositorios;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<ProjetoLocadoraDeVeiculosContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ProjetoLocadoraDeVeiculosContext"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("ProjetoLocadoraDeVeiculosContext")),
    builder => builder.MigrationsAssembly("ProjetoLocadoraDeVeiculos")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<ISessao, Sessao>();

builder.Services.AddSession(o =>
{
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
});

var app = builder.Build();



// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
var locale = builder.Configuration["SiteLocale"];
RequestLocalizationOptions localizationOptions = new RequestLocalizationOptions
{
    SupportedCultures = new List<CultureInfo> { new CultureInfo(locale) },
    SupportedUICultures = new List<CultureInfo> { new CultureInfo(locale) },
    DefaultRequestCulture = new RequestCulture(locale)
};

app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
