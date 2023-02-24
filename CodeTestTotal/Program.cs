using CodeTestTotal.Interfaces;
using CodeTestTotal.Models;
using CodeTestTotal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

/*Inject dependencies*/
var authenticatedUsersPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
/*Para no agregar la etiqueta authorize en todos los controladores, vamos a implementar el authorize a nivel global, a TODOS nuestros controladores*/
builder.Services.AddControllersWithViews(opciones =>
{
    opciones.Filters.Add(new AuthorizeFilter(authenticatedUsersPolicy));
});

builder.Services.AddSingleton<DBContext>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IClientService, ClientService>();
builder.Services.AddSingleton<IPetService, PetService>();
builder.Services.AddSingleton<IOrdenService, OrderService>();
builder.Services.AddSingleton<ISellerService, SellerService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserStore<Usuario>, UserStoreService>();
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

/*Configuracion para que nuestra aplicacion entienda el uso de cookies para autenticación, además vamos a configurar la ruta a la que el usuario será redirigido cuando no está autorizado*/
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme= IdentityConstants.ApplicationScheme;
}).AddCookie(IdentityConstants.ApplicationScheme, opciones =>
{
    opciones.LoginPath = "/User/Login";
});

builder.Services.ConfigureApplicationCookie(options => {
    options.AccessDeniedPath = "/User/Login";
});

builder.Services.AddIdentityCore<Usuario>();

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
