using CodeTestTotal.Interfaces;
using CodeTestTotal.Models;
using CodeTestTotal.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

/*Inject dependencies*/
builder.Services.AddSingleton<DBContext>();

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IClientService, ClientService>();
builder.Services.AddSingleton<IPetService, PetService>();
builder.Services.AddSingleton<IOrdenService, OrderService>();
builder.Services.AddSingleton<ISellerService, SellerService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IUserStore<Usuario>, UserStoreService>();
builder.Services.AddIdentityCore<Usuario>();
builder.Services.AddTransient<SignInManager<Usuario>>();

builder.Services.AddIdentityCore<Usuario>(opciones =>
{
    opciones.Password.RequireDigit = false;
    opciones.Password.RequireUppercase = false;
    opciones.Password.RequireLowercase = false;
    opciones.Password.RequireNonAlphanumeric = false;
});
/*Password rules*/

/*Configuracion para que nuestra aplicacion entienda el uso de cookies para autenticación*/
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme= IdentityConstants.ApplicationScheme;
}).AddCookie(IdentityConstants.ApplicationScheme);

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();
