using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using RVPark_Team2.Data;
using RVPark_Team2.Models;
using RVPark_Team2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// This is the builder that helps us hook up the database - Logan H.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Identity (this adds UserManager<ApplicationUser>, SignInManager, etc.)
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    {
        // change to true if you want confirmed email required for sign-in
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Register email sender used by the Identity UI (Register / ConfirmEmail)
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddScoped<ReservationService>();

// Add Razor Pages
builder.Services.AddRazorPages();

// Add controllers so API endpoints (e.g. /api/sites/availability) work
builder.Services.AddControllers();

// Add session support
builder.Services.AddDistributedMemoryCache(); // required for session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // adjust timeout if needed
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable session before Razor Pages
app.UseSession();

// Identity requires Authentication middleware before Authorization
app.UseAuthentication();
app.UseAuthorization();

// Map controller routes for API endpoints
app.MapControllers();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();