using ExamenZapasPartialViewsPaginacion.Data;
using ExamenZapasPartialViewsPaginacion.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string connectionString = builder.Configuration.GetConnectionString("SqlZapas");
builder.Services.AddDbContext<ZapasContext>(options => {
    options.UseSqlServer(connectionString);
});
builder.Services.AddTransient<ZapasRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Zapas}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
