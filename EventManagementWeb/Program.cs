using AspNetCore.Unobtrusive.Ajax;
using EventManagementWeb.Data;
using EventManagementWeb.Models;
using EventManagementWeb.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NETCore.MailKit.Infrastructure.Internal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<EventManagementUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddUnobtrusiveAjax();

//Restfull API
builder.Services.AddControllers();

builder.Services.AddTransient<IEmailSender, MailService>();
builder.Services.Configure<MailKitOptions>
    (
        options =>
        {
            //options.Server = builder.Configuration["ExternalProviders:MailKit:SMTP:Address"];
            //options.Port = Convert.ToInt32(builder.Configuration["ExternalProviders:MailKit:SMTP:Port"]);
            //options.Account = builder.Configuration["ExternalProviders:MailKit:SMTP:Account"];
            //options.Password = builder.Configuration["ExternalProviders:MailKit:SMTP:Password"];
            //options.SenderEmail = builder.Configuration["ExternalProviders:MailKit:SMTP:SenderEmail"];
            //options.SenderName = builder.Configuration["ExternalProviders:MailKit:SMTP:SenderName"];
        }
    );

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix) //language identifier suffix.
    .AddDataAnnotationsLocalization();  // Auto translate data annotations.

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EventManagementWeb", Version = "v1" });
});

builder.Services.AddTransient<IMyUser, MyUser>();

var app = builder.Build();
Globals.App = app;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EventManagementWeb v1"));
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    ApplicationDbContext context = new ApplicationDbContext(services.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
    var userManager = services.GetRequiredService<UserManager<EventManagementUser>>();
    SeedDataContext.Initialize(context, userManager);
}

var supportedCultures = new[] { "en", "fr", "nl" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseUnobtrusiveAjax();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.UseMyMiddleWare();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
