using CitaFacil.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CitaFacilContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")?? throw new InvalidOperationException("Connection string 'SqlServerConnection' not found.") ));
/*builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", config =>
    {
        config.Cookie.Name = "CitaFacil.Cookie";
        config.LoginPath = "/Home/Login";
    });*/
builder.Services.AddAuthorization(config =>
{
    config.AddPolicy("ClientePolicy", policy => 
    policy.RequireClaim("Cliente"));
    config.AddPolicy("NegocioPolicy", policy =>
    policy.RequireClaim("Negocio"));
    config.AddPolicy("AdminPolicy", policy =>
    policy.RequireClaim("Administrador"));
});
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
