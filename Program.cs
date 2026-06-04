using ARTNEST.DAL;
using ARTNEST.BLL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddScoped<IArtworkRepository, ArtworkRepository>();
builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJournalRepository, JournalRepository>();
builder.Services.AddScoped<IVisitedRepository, VisitedRepository>();
builder.Services.AddScoped<DbConnectionFactory>();
builder.Services.AddScoped<ArtworkService>();
builder.Services.AddScoped<WishlistService>();
builder.Services.AddScoped<VisitedService>();
builder.Services.AddScoped<JournalService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.MapRazorPages();
app.Run();
