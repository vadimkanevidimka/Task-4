using Microsoft.EntityFrameworkCore;
using Task_4.Interfaces;
using Task_4.Mocks;
using Task4DataBase.Context;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataBaseContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IUser, UserMocks>();
builder.Services.AddSession();
builder.Services.AddAuthentication("Bearer")  // схема аутентификации - с помощью jwt-токенов
    .AddJwtBearer();

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
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Users}/{id?}");

app.Run();
