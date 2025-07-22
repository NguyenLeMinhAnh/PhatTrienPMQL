using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using Microsoft.AspNetCore.Identity;
using DemoMVC.Models;
using Microsoft.AspNetCore.DataProtection;
using DemoMVC.Models.Process;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authorization;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOptions();
var mailSettings = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailSettings);
builder.Services.AddTransient<IEmailSender, SendMailService>();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<IdentityOptions>(options =>
{
    //Default Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    //Config Password
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;

    //Cofig Login
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    //Config User
    options.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    //Chi gui Cookie qua HTTPS
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    //Giam thieu rui ro CSRF
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
});
builder.Services.AddAuthorization(options =>
        {
            foreach (var permission in Enum.GetValues(typeof(SystemPermissions)).Cast<SystemPermissions>())
            {
                options.AddPolicy(permission.ToString(), policy =>
                    policy.RequireClaim("Permission", permission.ToString()));
            }
            // options.AddPolicy("ViewEmployee", policy => policy.RequireClaim("Employee", "Index"));
            // options.AddPolicy("CreateEmployee", policy => policy.RequireClaim("Employee", "Create"));
            // options.AddPolicy("Role", policy => policy.RequireClaim("Role", "AdminOnly"));
            // options.AddPolicy("Permission", policy => policy.RequireClaim("Role", "EmployeeOnly"));
            // options.AddPolicy("PolicyAdmin", policy => policy.RequireRole("Admin"));
            // options.AddPolicy("PolicyEmployee", policy => policy.RequireRole("Employee"));
            // options.AddPolicy("PolicyByPhoneNumber", policy => policy.Requirements.Add(new PolicyByPhoneNumberRequirement()));
        });

builder.Services.AddDataProtection()
    // Chi dinh thu muc luu tru khoa bao ve du lieu
    .PersistKeysToFileSystem(new DirectoryInfo(@"./keys"))
    //xac dinh ten ung dung su dung dich vu bao ve du lieu
    .SetApplicationName("YourAppName")
    //dat thoi gian so cho khoa bao mat du lieu
    .SetDefaultKeyLifetime(TimeSpan.FromDays(14));

builder.Services.AddTransient<EmployeesSeeder>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seeder = services.GetRequiredService<EmployeesSeeder>();
    seeder.SeedEmloyees(1000);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

app.Run();
