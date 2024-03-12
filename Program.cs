using BoxBox.Data;
using BoxBox.Helpers;
using BoxBox.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<RepositoryBoxBox>();
builder.Services.AddTransient<RepositoryAuth>();

string connectionString = builder.Configuration.GetConnectionString("SqlBoxBox");

builder.Services.AddDbContext<BoxBoxContext>
    (options => options.UseSqlServer(connectionString));

builder.Services.AddSingleton<HelperPathProvider>();
builder.Services.AddSingleton<HelperUploadFiles>();

builder.Services.AddSession();

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
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
