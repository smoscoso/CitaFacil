using CitaFacil.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CitaFacil.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.CodeAnalysis.Options;
using CitaFacil.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CitaFacilContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SomeeConnection")));
builder.Services.AddScoped<IUsuarioService, UsuarioServices>();
builder.Services.AddScoped<IEmailService, EMailServices>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "CitaFacilCookie"; // Establece el esquema predeterminado
    options.DefaultSignInScheme = "CitaFacilCookie"; // Establece el esquema para inicio de sesión

})
        .AddCookie("CitaFacilCookie", options =>
        {
            options.LoginPath = "/Home/Index"; // Página de inicio de sesión
            options.AccessDeniedPath = "/Home/Index"; // Página de acceso denegado
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequiereLogin", policy => policy.RequireAuthenticatedUser());
});
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<CitaFacilContext>();
        //context.Database.EnsureDeleted();
        //context.Database.EnsureCreated();
        SeedData.Initialize(services);
    }catch (SqlException ex)
    {
        Console.WriteLine(ex.Message);
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
