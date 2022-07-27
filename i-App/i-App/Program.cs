global using i_App.Data.models;
global using i_App.Controllers.UI.SideBar;
global using i_App.Services.Vehicle;
global using i_App.Services.Department;
global using i_App.View.Model;
global using i_App.Controllers.UI.Tables;
global using i_App.Services.AssetsDB;
global using i_App.Services.AuthorizeDB;
global using i_App.Data.State;
global using i_App.Controllers.UI.TableToggle;
global using i_App.Services.InsuranceDB;
global using i_App.Shared.EventCallBacks;
using i_App.Hubs;
using i_App.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.ResponseCompression;


var builder = WebApplication.CreateBuilder(args);

// Authentication Coookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();



// Added the Configuration service
builder.Services.AddDbContextFactory<InsuranceContext>(opt => 
        opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));




builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<AppState>();
builder.Services.AddScoped<VehicleService>();
builder.Services.AddScoped<VehicleRepository>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<DepartmentRepository>();
builder.Services.AddScoped<AssetService>();
builder.Services.AddScoped<AssetRepository>();
builder.Services.AddScoped<AuthorizeService>();
builder.Services.AddScoped<AuthorizeRepository>();
builder.Services.AddScoped<InsuranceService>();
builder.Services.AddScoped<InsuranceRepository>();

builder.Services.AddSignalR();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});


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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapHub<ChatHub>("/chathub");
app.MapFallbackToPage("/_Host");

app.Run();
